using OpenAdm.Cep.Application.Dtos.Fretes;
using OpenAdm.Cep.Application.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO.Compression;

namespace OpenAdm.Cep.Application.Services;

public sealed class ChromeService : IChromeService, IDisposable
{
    private readonly ChromeDriver _driver;
    private readonly bool _isWindows = Environment.OSVersion.VersionString.Contains("Windows");

    private static void DownloadAndExtractFile(string url, string zipFilePath, string extractPath)
    {
        using var httpClient = new HttpClient();

        var response = httpClient.GetAsync(url).Result;
        if (response.IsSuccessStatusCode)
        {
            using (var fileStream = new FileStream(zipFilePath, FileMode.Create, FileAccess.Write))
            {
                response.Content.CopyToAsync(fileStream).Wait();
            }

            ExtractZipFile(zipFilePath, extractPath);
        }
    }

    private static void ExtractZipFile(string zipFilePath, string extractPath)
    {
        using var archive = ZipFile.OpenRead(zipFilePath);

        foreach (ZipArchiveEntry entry in archive.Entries)
        {
            string entryPath = Path.Combine(extractPath, entry.Name.Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (string.IsNullOrEmpty(entry.Name))
            {
                Directory.CreateDirectory(entryPath);
            }
            else
            {
                entry.ExtractToFile(entryPath, overwrite: true);
            }
        }
    }

    public ChromeService()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        string driverDirectory = !_isWindows ? $"{Directory.GetCurrentDirectory()}/chromedriver" : $"{Directory.GetCurrentDirectory()}\\Driver\\chromedriver.exe";

        if (!File.Exists(driverDirectory))
        {
            string url = _isWindows ? "https://storage.googleapis.com/chrome-for-testing-public/126.0.6478.126/win64/chromedriver-win64.zip" 
                : "https://storage.googleapis.com/chrome-for-testing-public/126.0.6478.126/linux64/chromedriver-linux64.zip";
            string zipFilePath = Path.Combine(Directory.GetCurrentDirectory(), "chromedriver.zip");
            string extractPath = !_isWindows ? "/" : "\\Driver";
            var diretorio = $"{Directory.GetCurrentDirectory()}{extractPath}";

            try
            {
                Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}{extractPath}");
                DownloadAndExtractFile(url, zipFilePath, diretorio);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao baixar e extrair o ChromeDriver: {ex.Message}");
            }
        }

        _driver ??= new ChromeDriver(driverDirectory, options);
    }

    public async Task<decimal> CalcularFreteAsync(CalcularFreteDto calcularFreteDto)
    {
        calcularFreteDto.Validar();
        await _driver.Navigate().GoToUrlAsync("https://www2.correios.com.br/sistemas/precosPrazos");
        var cepOrigem = _driver.FindElement(By.Name("cepOrigem"));
        cepOrigem.Click();
        cepOrigem.SendKeys(calcularFreteDto.CepOrigem);

        var cepDestino = _driver.FindElement(By.Name("cepDestino"));
        cepDestino.Click();
        cepDestino.SendKeys(calcularFreteDto.CepDestino);

        _driver.FindElement(By.XPath($"//html/body/div[1]/div[3]/div[2]/div/div/div[2]/div[2]/div[3]/form/div/div/span[7]/label/select/option[{calcularFreteDto.TipoFrete}]")).Click();
        _driver.ExecuteScript("EnviaFormato(1);");
        _driver.FindElement(By.XPath("//*[@id=\"spanEmbalagemCaixa\"]/label/select/option[3]")).Click();
        var altura = _driver.FindElement(By.Name("Altura"));
        altura.Click();
        altura.SendKeys(calcularFreteDto.Altura);
        var largura = _driver.FindElement(By.Name("Largura"));
        largura.Click();
        largura.SendKeys(calcularFreteDto.Largura);
        var comprimento = _driver.FindElement(By.Name("Comprimento"));
        comprimento.Click();
        comprimento.SendKeys(calcularFreteDto.Comprimento);
        _driver.FindElement(By.XPath($"//*[@id=\"spanServicoSelecionado\"]/span[5]/label/select/option[{calcularFreteDto.Peso + 2}]")).Click();
        var form = _driver.FindElement(By.TagName("form"));
        _driver.ExecuteScript("arguments[0].setAttribute('target', '')", form);
        var btn = _driver.FindElement(By.Name("Calcular"));
        btn.Click();
        var table = _driver.FindElement(By.TagName("table"));
        var footer = table.FindElement(By.TagName("tfoot"));
        var tr = footer.FindElement(By.TagName("tr"));
        var td = tr.FindElement(By.TagName("td")).Text;
        decimal frete;

        if (_isWindows)
        {
            _ = decimal.TryParse(td.Replace(".", "").Replace("R$", "").Replace(" ", "").Trim(), out frete);
        }
        else
        {
            _ = decimal.TryParse(td.Replace(",", ".").Replace("R$", "").Replace(" ", "").Trim(), out frete);
        }

        return frete;
    }

    public void Dispose()
    {
        _driver.Quit();
    }

    public void Quit()
    {
        _driver.Quit();
    }
}

using OpenAdm.Cep.Application.Dtos.Fretes;
using OpenAdm.Cep.Application.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace OpenAdm.Cep.Application.Services;

public sealed class ChromeService : IChromeService, IDisposable
{
    private readonly ChromeDriver _driver;
    private readonly bool _isWindows = Environment.OSVersion.VersionString.Contains("Windows");
    public ChromeService()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        string driverDirectory = !_isWindows ? $"{Directory.GetCurrentDirectory()}/chromedriver" : $"{Directory.GetCurrentDirectory()}\\Driver\\chromedriver.exe";
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

        _driver.FindElement(By.XPath("//html/body/div[1]/div[3]/div[2]/div/div/div[2]/div[2]/div[3]/form/div/div/span[7]/label/select/option[24]")).Click();
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

using OpenAdm.Cep.Application.Dtos.Fretes;
using OpenAdm.Cep.Application.Services;
using OpenAdm.Cep.Domain.Exceptions;

namespace OpenAdm.Cep.Test.Application;

public class ChromeServiceTest : IDisposable
{
    private readonly ChromeService _chromeService = new();
    public void Dispose()
    {
        _chromeService.Quit();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Nao_Deve_Consultar_Frete_Com_Peso_Invalido(int peso)
    {
        var calcularFreteDto = new CalcularFreteDto()
        {
            Peso = peso,
        };

        await Assert.ThrowsAsync<CepException>(async () => await _chromeService.CalcularFreteAsync(calcularFreteDto));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaa")]
    public async Task Nao_Deve_Consultar_Frete_Com_Cep_Invalido(string cep)
    {
        var calcularFreteDto = new CalcularFreteDto()
        {
            Peso = 10,
            CepDestino = cep,
        };

        await Assert.ThrowsAsync<CepException>(async () => await _chromeService.CalcularFreteAsync(calcularFreteDto));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaa")]
    public async Task Nao_Deve_Consultar_Frete_Com_Altura_Invalido(string altura)
    {
        var calcularFreteDto = new CalcularFreteDto()
        {
            Peso = 10,
            CepDestino = "88316301",
            CepOrigem = "88316301",
            Altura = altura
        };

        await Assert.ThrowsAsync<CepException>(async () => await _chromeService.CalcularFreteAsync(calcularFreteDto));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaa")]
    public async Task Nao_Deve_Consultar_Frete_Com_Largura_Invalido(string largura)
    {
        var calcularFreteDto = new CalcularFreteDto()
        {
            Peso = 10,
            CepDestino = "88316301",
            CepOrigem = "88316301",
            Altura = "10",
            Largura = largura
        };

        await Assert.ThrowsAsync<CepException>(async () => await _chromeService.CalcularFreteAsync(calcularFreteDto));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaa")]
    public async Task Nao_Deve_Consultar_Frete_Com_Comprimento_Invalido(string comprimento)
    {
        var calcularFreteDto = new CalcularFreteDto()
        {
            Peso = 10,
            CepDestino = "88316301",
            CepOrigem = "88316301",
            Altura = "10",
            Largura = "11",
            Comprimento = comprimento
        };

        await Assert.ThrowsAsync<CepException>(async () => await _chromeService.CalcularFreteAsync(calcularFreteDto));
    }
}

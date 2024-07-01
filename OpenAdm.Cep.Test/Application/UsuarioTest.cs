using Moq;
using OpenAdm.Cep.Application.Dtos.Usuarios;
using OpenAdm.Cep.Application.Services;
using OpenAdm.Cep.Domain.Exceptions;
using OpenAdm.Cep.Domain.Interfaces;
using OpenAdm.Cep.Test.Build;

namespace OpenAdm.Cep.Test.Application;

public class UsuarioTest
{
    private readonly UsuarioService _service;
    private readonly Mock<IUsuarioRepository> _repository;

    public UsuarioTest()
    {
        _repository = new();
        _service = new UsuarioService(_repository.Object);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaa")]
    public async Task Nao_Deve_Criar_Usuario_Com_Nome_Invalido(string? nome)
    {
        var dto = new UsuarioCreateDto()
        {
            Nome = nome!
        };
        await Assert.ThrowsAsync<CepException>(async () => await _service.CreateUsuarioAsync(dto));
    }

    [Fact]
    public async Task Deve_Criar_Usuario()
    {
        var dto = new UsuarioCreateDto()
        {
            Nome = "Test 1"
        };

        var response = await _service.CreateUsuarioAsync(dto);

        Assert.NotNull(response);
        Assert.True(Guid.Empty != response.ChaveDeAcesso);
    }

    [Fact]
    public async Task Nao_Deve_Criar_Usuario_Com_Nome_Igual()
    {
        var dto = new UsuarioCreateDto()
        {
            Nome = "Test 1"
        };
        var usuario = UsuarioBuild.Init().AddNome(dto.Nome).Build();
        _repository.Setup((x) => x.GetByNomeAsync(dto.Nome)).ReturnsAsync(usuario);
        await Assert.ThrowsAsync<CepException>(async () => await _service.CreateUsuarioAsync(dto));
    }
}

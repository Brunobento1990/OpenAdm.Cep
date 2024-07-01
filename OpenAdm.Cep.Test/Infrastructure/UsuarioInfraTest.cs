using OpenAdm.Cep.Infrastructure.Repositories;
using OpenAdm.Cep.Test.Build;

namespace OpenAdm.Cep.Test.Infrastructure;

public class UsuarioInfraTest
{
    private readonly UsuarioRepository _usuarioRepository;

    public UsuarioInfraTest()
    {
        _usuarioRepository = new UsuarioRepository(AppDoContextBuild.Init().Build());
    }

    [Fact]
    public async Task Deve_Adicionar_Usuario_No_Banco()
    {
        var usuario = UsuarioBuild.Init().Build();
        await _usuarioRepository.AddAsync(usuario);
        var usuarioValidacao = await _usuarioRepository.GetByIdAsync(usuario.Id);

        Assert.NotNull(usuarioValidacao);
        Assert.Equal(usuario.Id, usuarioValidacao.Id);
    }
}

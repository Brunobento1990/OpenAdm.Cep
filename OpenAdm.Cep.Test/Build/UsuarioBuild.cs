using OpenAdm.Cep.Domain.Entities;

namespace OpenAdm.Cep.Test.Build;

public class UsuarioBuild
{
    private string _nome;

    public UsuarioBuild()
    {
        _nome = "Teste 2";
    }

    public static UsuarioBuild Init() => new();

    public UsuarioBuild AddNome(string nome)
    {
        _nome = nome;
        return this;
    }

    public Usuario Build()
    {
        return new Usuario(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.Now,
            dataDeAtualizacao: null,
            nome: _nome);
    }
}

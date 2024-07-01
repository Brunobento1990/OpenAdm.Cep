
namespace OpenAdm.Cep.Domain.Entities;

public sealed class Usuario : BaseEntity
{
    public Usuario(
        Guid id,
        DateTime dataDeCadastro,
        DateTime? dataDeAtualizacao,
        string nome)
            : base(id, dataDeCadastro, dataDeAtualizacao)
    {
        Nome = nome;
    }

    public string Nome { get; private set; }
}

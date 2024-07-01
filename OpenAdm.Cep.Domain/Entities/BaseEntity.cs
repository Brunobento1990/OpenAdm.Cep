namespace OpenAdm.Cep.Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity(
        Guid id, 
        DateTime dataDeCadastro, 
        DateTime? dataDeAtualizacao)
    {
        Id = id;
        DataDeCadastro = dataDeCadastro;
        DataDeAtualizacao = dataDeAtualizacao;
    }

    public Guid Id { get; protected set; }
    public DateTime DataDeCadastro { get; protected set; }
    public DateTime? DataDeAtualizacao { get; protected set; }
}

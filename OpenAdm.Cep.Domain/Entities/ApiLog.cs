namespace OpenAdm.Cep.Domain.Entities;

public sealed class ApiLog
{
    public ApiLog(
        Guid id,
        string origem,
        string host,
        string ip,
        string path,
        string? erro,
        int statusCode,
        int logLevel,
        Guid usuarioId,
        DateTime dataDeCadastro)
    {
        Id = id;
        Origem = origem;
        Host = host;
        Ip = ip;
        Path = path;
        Erro = erro;
        StatusCode = statusCode;
        LogLevel = logLevel;
        UsuarioId = usuarioId;
        DataDeCadastro = dataDeCadastro;
    }
    public Guid Id { get; private set; }
    public string Origem { get; private set; }
    public string Host { get; private set; }
    public string Ip { get; private set; }
    public string Path { get; private set; }
    public string? Erro { get; private set; }
    public int StatusCode { get; private set; }
    public int LogLevel { get; private set; }
    public DateTime DataDeCadastro { get; private set; }
    public Guid UsuarioId { get; private set; }
}

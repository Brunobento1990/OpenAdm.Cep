using OpenAdm.Cep.Application.Dtos.Logs;
using OpenAdm.Cep.Application.Interfaces;
using OpenAdm.Cep.Domain.Entities;
using OpenAdm.Cep.Domain.Interfaces;

namespace OpenAdm.Cep.Application.Services;

public sealed class ApiLogService : IApiLogService
{
    private readonly IApiLogRepository _apiLogRepository;

    public ApiLogService(IApiLogRepository apiLogRepository)
    {
        _apiLogRepository = apiLogRepository;
    }

    public async Task CreateAppLogAsync(CreateAppLog createAppLog)
    {
        var log = new ApiLog(
            id: Guid.NewGuid(), 
            origem: createAppLog.Origem, 
            host: createAppLog.Host,
            ip: createAppLog.Ip,
            path: createAppLog.Path,
            erro: createAppLog.Erro,
            statusCode: createAppLog.StatusCode,
            logLevel: createAppLog.LogLevel,
            usuarioId: Guid.Empty,
            dataDeCadastro: DateTime.Now);

        await _apiLogRepository.AddLogAsync(log);
    }
}

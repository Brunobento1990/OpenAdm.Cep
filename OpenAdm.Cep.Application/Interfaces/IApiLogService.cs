using OpenAdm.Cep.Application.Dtos.Logs;

namespace OpenAdm.Cep.Application.Interfaces;

public interface IApiLogService
{
    Task CreateAppLogAsync(CreateAppLog createAppLog);
}

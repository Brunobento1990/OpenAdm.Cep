using OpenAdm.Cep.Domain.Entities;

namespace OpenAdm.Cep.Domain.Interfaces;

public interface IApiLogRepository
{
    Task AddLogAsync(ApiLog apiLog);
}

using Microsoft.Extensions.DependencyInjection;
using OpenAdm.Cep.Domain.Entities;
using OpenAdm.Cep.Domain.Interfaces;
using OpenAdm.Cep.Infrastructure.Context;

namespace OpenAdm.Cep.Infrastructure.Repositories;

public sealed class ApiLogRepository : IApiLogRepository
{
    private readonly IServiceProvider _serviceProvider;

    public ApiLogRepository(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task AddLogAsync(ApiLog apiLog)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.AddAsync(apiLog);
        await context.SaveChangesAsync();
    }
}

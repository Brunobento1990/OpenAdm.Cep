using Microsoft.Extensions.DependencyInjection;
using OpenAdm.Cep.Domain.Interfaces;
using OpenAdm.Cep.Infrastructure.Repositories;

namespace OpenAdm.Cep.IoC;

public static class DependencyInjectRepositories
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<IApiLogRepository, ApiLogRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }
}

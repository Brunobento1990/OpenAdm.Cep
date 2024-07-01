using Microsoft.Extensions.DependencyInjection;
using OpenAdm.Cep.Application.Interfaces;
using OpenAdm.Cep.Application.Services;

namespace OpenAdm.Cep.IoC;

public static class DependencyInjectServices
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddSingleton<IChromeService, ChromeService>();
        services.AddScoped<ICepService, CepService>();
        services.AddScoped<IApiLogService, ApiLogService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IFreteService, FreteService>();

        return services;
    }
}

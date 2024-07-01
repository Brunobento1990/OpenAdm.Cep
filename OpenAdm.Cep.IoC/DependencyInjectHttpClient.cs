using Microsoft.Extensions.DependencyInjection;

namespace OpenAdm.Cep.IoC;

public static class DependencyInjectHttpClient
{
    public static IServiceCollection InjectViaCep(this IServiceCollection services, string url)
    {
        services.AddHttpClient("VIA_CEP", opt =>
        {
            opt.BaseAddress = new Uri(url);
        });

        return services;
    }
}

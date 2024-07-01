using Microsoft.Extensions.DependencyInjection;

namespace OpenAdm.Cep.IoC;

public static class DependencyInjectCors
{
    public static IServiceCollection InjectCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "base",
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                              });
        });

        return services;
    }
}

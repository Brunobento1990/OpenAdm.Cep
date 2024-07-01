using System.Text.Json.Serialization;

namespace OpenAdm.Cep.Api.Configurations;

public static class ConfigurationController
{
    public static IServiceCollection ConfigureController(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddControllers()
            .AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        return services;
    }
}

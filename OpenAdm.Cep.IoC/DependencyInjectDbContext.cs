using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenAdm.Cep.Infrastructure.Context;

namespace OpenAdm.Cep.IoC;

public static class DependencyInjectDbContext
{
    public static IServiceCollection InjectContext(this IServiceCollection services, string connectionString)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
        return services;
    }
}

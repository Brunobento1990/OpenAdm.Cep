using Microsoft.EntityFrameworkCore;
using OpenAdm.Cep.Infrastructure.Context;

namespace OpenAdm.Cep.Test.Build;

public class AppDoContextBuild
{
    public static AppDoContextBuild Init() => new();

    public AppDbContext Build()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseInMemoryDatabase("open-adm-cep");
        return new AppDbContext(options.Options);
    }
}

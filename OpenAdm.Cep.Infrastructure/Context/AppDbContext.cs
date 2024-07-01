using Microsoft.EntityFrameworkCore;
using OpenAdm.Cep.Domain.Entities;
using OpenAdm.Cep.Infrastructure.Configuration;

namespace OpenAdm.Cep.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<ApiLog> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApiLogConfiguration());
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
    }
}

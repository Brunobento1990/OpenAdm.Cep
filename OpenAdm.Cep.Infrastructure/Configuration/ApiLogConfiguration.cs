using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenAdm.Cep.Domain.Entities;

namespace OpenAdm.Cep.Infrastructure.Configuration;

public class ApiLogConfiguration : IEntityTypeConfiguration<ApiLog>
{
    public void Configure(EntityTypeBuilder<ApiLog> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Origem)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(x => x.Host)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(x => x.Ip)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(x => x.Path)
            .IsRequired()
            .HasMaxLength(1000);
        builder.HasIndex(x => x.StatusCode);
        builder.HasIndex(x => x.LogLevel);
        builder.HasIndex(x => x.UsuarioId);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenAdm.Cep.Domain.Entities;

namespace OpenAdm.Cep.Infrastructure.Configuration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DataDeCadastro)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("now()");
        builder.Property(x => x.DataDeAtualizacao)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("now()");
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(x => x.Nome);
    }
}

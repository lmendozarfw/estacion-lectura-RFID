using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.Code)
            .HasColumnName("codigo")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("descripcion")
            .HasMaxLength(500);

        builder.Property(x => x.Active)
            .HasColumnName("activo")
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();
    }
}

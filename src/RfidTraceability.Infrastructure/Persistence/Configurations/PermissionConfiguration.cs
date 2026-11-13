using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permisos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.ModuleId)
            .HasColumnName("id_modulo")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasColumnName("codigo")
            .HasMaxLength(100)
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

        builder.HasOne(x => x.Module)
            .WithMany(x => x.Permissions)
            .HasForeignKey(x => x.ModuleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.ModuleId, x.Code })
            .IsUnique();
    }
}

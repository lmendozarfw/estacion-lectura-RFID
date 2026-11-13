using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("roles_permisos");

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.Property(x => x.RoleId)
            .HasColumnName("id_rol")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.PermissionId)
            .HasColumnName("id_permiso")
            .HasMaxLength(36)
            .IsRequired();

        builder.HasOne(x => x.Role)
            .WithMany(x => x.RolePermissions)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Permission)
            .WithMany(x => x.RolePermissions)
            .HasForeignKey(x => x.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

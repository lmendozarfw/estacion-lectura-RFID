using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("usuarios_roles");

        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.Property(x => x.UserId)
            .HasColumnName("id_usuario")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.RoleId)
            .HasColumnName("id_rol")
            .HasMaxLength(36)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

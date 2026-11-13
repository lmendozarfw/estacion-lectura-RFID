using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.OrganizationId)
            .HasColumnName("id_organizacion")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.Username)
            .HasColumnName("username")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.EmployeeId)
            .HasColumnName("employee_id")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Active)
            .HasColumnName("activo")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("fecha_creacion")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("fecha_actualizacion")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.OrganizationId, x.Username })
            .IsUnique();
    }
}

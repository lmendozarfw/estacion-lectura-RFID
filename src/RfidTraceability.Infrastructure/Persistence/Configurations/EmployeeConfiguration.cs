using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("empleados");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.EmployeeNumber)
            .HasColumnName("numero_empleado")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasColumnName("nombres")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.PaternalSurname)
            .HasColumnName("apellido_paterno")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.MaternalSurname)
            .HasColumnName("apellido_materno")
            .HasMaxLength(200)
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

        builder.HasIndex(x => x.EmployeeNumber)
            .IsUnique();
    }
}

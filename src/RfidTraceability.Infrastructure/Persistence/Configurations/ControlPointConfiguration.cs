using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class ControlPointConfiguration : IEntityTypeConfiguration<ControlPoint>
{
    public void Configure(EntityTypeBuilder<ControlPoint> builder)
    {
        builder.ToTable("puntos_control");

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

        builder.Property(x => x.BusinessUnitId)
            .HasColumnName("id_unidad_negocio")
            .HasMaxLength(36)
            .IsRequired();

        builder.HasOne(x => x.BusinessUnit)
            .WithMany(x => x.ControlPoints)
            .HasForeignKey(x => x.BusinessUnitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.BusinessUnitId, x.Code })
            .IsUnique();
    }
}

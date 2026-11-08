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

        builder.Property(x => x.Name)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();
    }
}

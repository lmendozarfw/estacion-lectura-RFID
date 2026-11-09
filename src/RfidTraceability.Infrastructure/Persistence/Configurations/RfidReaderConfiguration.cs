using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Infrastructure.Persistence.Converters;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class RfidReaderConfiguration : IEntityTypeConfiguration<RfidReader>
{
    public void Configure(EntityTypeBuilder<RfidReader> builder)
    {
        builder.ToTable("lectores_rfid");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.Code)
            .HasColumnName("codigo")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.SerialNumber)
            .HasColumnName("numero_serie")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("estado")
            .HasMaxLength(20)
            .HasConversion<ReaderStatusConverter>()
            .HasDefaultValueSql("'ACTIVO'")
            .IsRequired();

        builder.Property(x => x.ControlPointId)
            .HasColumnName("id_punto_control")
            .HasMaxLength(36)
            .IsRequired();

        builder.HasOne(x => x.ControlPoint)
            .WithMany(x => x.Readers)
            .HasForeignKey(x => x.ControlPointId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.SerialNumber)
            .IsUnique();

        builder.HasIndex(x => new { x.ControlPointId, x.Code })
            .IsUnique();
    }
}

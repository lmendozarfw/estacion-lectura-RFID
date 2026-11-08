using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class PieceControlPointAdvanceReadingConfiguration : IEntityTypeConfiguration<PieceControlPointAdvanceReading>
{
    public void Configure(EntityTypeBuilder<PieceControlPointAdvanceReading> builder)
    {
        builder.ToTable("lecturas_avance_pieza_punto_control");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.RfidPieceId)
            .HasColumnName("id_pieza_rfid")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.ControlPointId)
            .HasColumnName("id_punto_control")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.Date)
            .HasColumnName("fecha")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.HasOne(x => x.RfidPiece)
            .WithMany(x => x.AdvanceReadings)
            .HasForeignKey(x => x.RfidPieceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ControlPoint)
            .WithMany(x => x.AdvanceReadings)
            .HasForeignKey(x => x.ControlPointId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.RfidPieceId, x.ControlPointId })
            .IsUnique();
    }
}

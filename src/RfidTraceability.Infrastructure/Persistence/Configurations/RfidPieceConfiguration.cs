using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class RfidPieceConfiguration : IEntityTypeConfiguration<RfidPiece>
{
    public void Configure(EntityTypeBuilder<RfidPiece> builder)
    {
        builder.ToTable("piezas_rfid");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.Epc)
            .HasColumnName("epc")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.SkuId)
            .HasColumnName("id_sku")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.LotId)
            .HasColumnName("id_lote")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.SideCode)
            .HasColumnName("codigo_lado")
            .HasMaxLength(2)
            .IsRequired();

        builder.HasOne(x => x.Sku)
            .WithMany(x => x.RfidPieces)
            .HasForeignKey(x => x.SkuId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Lot)
            .WithMany(x => x.RfidPieces)
            .HasForeignKey(x => x.LotId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Epc)
            .IsUnique();

        builder.HasIndex(x => new { x.LotId, x.SideCode })
            .IsUnique();
    }
}

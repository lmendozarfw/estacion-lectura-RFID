using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class LotConfiguration : IEntityTypeConfiguration<Lot>
{
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
        builder.ToTable("lotes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.Code)
            .HasColumnName("codigo")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ExpectedPairs)
            .HasColumnName("cantidad_pares")
            .IsRequired();

        builder.Property(x => x.BlockId)
            .HasColumnName("id_bloque")
            .HasMaxLength(36)
            .IsRequired();

        builder.HasOne(x => x.Block)
            .WithMany(x => x.Lots)
            .HasForeignKey(x => x.BlockId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.BlockId, x.Code })
            .IsUnique();
    }
}

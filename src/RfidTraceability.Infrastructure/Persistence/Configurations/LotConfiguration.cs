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

        builder.Property(x => x.ProgramId)
            .HasColumnName("id_programa")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.ExpectedPieces)
            .HasColumnName("cantidad_piezas")
            .IsRequired();

        builder.HasOne(x => x.Program)
            .WithMany(x => x.Lots)
            .HasForeignKey(x => x.ProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.ProgramId, x.Code })
            .IsUnique();
    }
}

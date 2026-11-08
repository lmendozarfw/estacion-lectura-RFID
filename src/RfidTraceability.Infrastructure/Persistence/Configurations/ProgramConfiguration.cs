using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class ProgramConfiguration : IEntityTypeConfiguration<Program>
{
    public void Configure(EntityTypeBuilder<Program> builder)
    {
        builder.ToTable("programas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.Code)
            .HasColumnName("codigo")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.PurchaseOrderId)
            .HasColumnName("id_orden_compra")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .HasColumnName("fecha_inicio")
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.EndDate)
            .HasColumnName("fecha_fin")
            .HasColumnType("timestamp with time zone");

        builder.HasOne(x => x.PurchaseOrder)
            .WithMany(x => x.Programs)
            .HasForeignKey(x => x.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.PurchaseOrderId, x.Code })
            .IsUnique();
    }
}

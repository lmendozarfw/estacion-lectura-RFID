using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Infrastructure.Persistence.Converters;

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

        builder.Property(x => x.TargetQuantity)
            .HasColumnName("cantidad_objetivo")
            .IsRequired();

        builder.Property(x => x.PlannedStartDate)
            .HasColumnName("fecha_inicio_planificada")
            .HasColumnType("date");

        builder.Property(x => x.PlannedEndDate)
            .HasColumnName("fecha_fin_planificada")
            .HasColumnType("date");

        builder.Property(x => x.ActualStartDate)
            .HasColumnName("fecha_inicio_real")
            .HasColumnType("date");

        builder.Property(x => x.ActualEndDate)
            .HasColumnName("fecha_fin_real")
            .HasColumnType("date");

        builder.Property(x => x.Status)
            .HasColumnName("estado")
            .HasMaxLength(20)
            .HasConversion<ProgramStatusConverter>()
            .HasDefaultValueSql("'PLANIFICADO'")
            .IsRequired();

        builder.Property(x => x.PurchaseOrderId)
            .HasColumnName("id_orden_compra")
            .HasMaxLength(36)
            .IsRequired();

        builder.HasOne(x => x.PurchaseOrder)
            .WithMany(x => x.Programs)
            .HasForeignKey(x => x.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.PurchaseOrderId, x.Code })
            .IsUnique();
    }
}

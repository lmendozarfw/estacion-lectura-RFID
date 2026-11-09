using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Infrastructure.Persistence.Converters;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        builder.ToTable("ordenes_compra");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.Code)
            .HasColumnName("codigo")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Date)
            .HasColumnName("fecha")
            .HasColumnType("date");

        builder.Property(x => x.OrganizationId)
            .HasColumnName("id_organizacion")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.Origin)
            .HasColumnName("origen")
            .HasMaxLength(20)
            .HasConversion<DataOriginConverter>()
            .HasDefaultValueSql("'LOCAL'")
            .IsRequired();

        builder.Property(x => x.ExternalId)
            .HasColumnName("id_externo")
            .HasMaxLength(100);

        builder.HasOne(x => x.Organization)
            .WithMany(x => x.PurchaseOrders)
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.OrganizationId, x.Code })
            .IsUnique();

        builder.HasIndex(x => new { x.OrganizationId, x.ExternalId })
            .IsUnique();
    }
}

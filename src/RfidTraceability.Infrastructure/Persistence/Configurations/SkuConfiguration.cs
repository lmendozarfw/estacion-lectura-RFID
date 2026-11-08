using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class SkuConfiguration : IEntityTypeConfiguration<Sku>
{
    public void Configure(EntityTypeBuilder<Sku> builder)
    {
        builder.ToTable("skus");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.Code)
            .HasColumnName("codigo")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();
    }
}

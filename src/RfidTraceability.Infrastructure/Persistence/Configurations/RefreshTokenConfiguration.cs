using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("tokens_refresco");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .HasColumnName("id_usuario")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.TokenHash)
            .HasColumnName("token_hash")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("fecha_creacion")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(x => x.ExpiresAt)
            .HasColumnName("fecha_expiracion")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(x => x.RevokedAt)
            .HasColumnName("fecha_revocacion")
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.ReplacedByTokenHash)
            .HasColumnName("reemplazado_por")
            .HasMaxLength(128);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.TokenHash)
            .IsUnique();

        builder.HasIndex(x => x.UserId);
    }
}

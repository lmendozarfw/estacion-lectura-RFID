using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence.Configurations;

public class ProgramGoalConfiguration : IEntityTypeConfiguration<ProgramGoal>
{
    public void Configure(EntityTypeBuilder<ProgramGoal> builder)
    {
        builder.ToTable("metas_programa");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(36)
            .ValueGeneratedNever();

        builder.Property(x => x.ProgramId)
            .HasColumnName("id_programa")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.ControlPointId)
            .HasColumnName("id_punto_control")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.Date)
            .HasColumnName("fecha")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.TargetQuantity)
            .HasColumnName("cantidad_meta")
            .IsRequired();

        builder.HasOne(x => x.Program)
            .WithMany(x => x.Goals)
            .HasForeignKey(x => x.ProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ControlPoint)
            .WithMany(x => x.Goals)
            .HasForeignKey(x => x.ControlPointId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.ProgramId, x.ControlPointId, x.Date })
            .IsUnique();
    }
}

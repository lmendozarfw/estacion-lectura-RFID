using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<Program> Programs => Set<Program>();
    public DbSet<Lot> Lots => Set<Lot>();
    public DbSet<Sku> Skus => Set<Sku>();
    public DbSet<ControlPoint> ControlPoints => Set<ControlPoint>();
    public DbSet<RfidPiece> RfidPieces => Set<RfidPiece>();
    public DbSet<PieceControlPointAdvanceReading> PieceControlPointAdvanceReadings => Set<PieceControlPointAdvanceReading>();
    public DbSet<ProgramGoal> ProgramGoals => Set<ProgramGoal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

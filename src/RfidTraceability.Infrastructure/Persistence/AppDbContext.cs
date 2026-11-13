using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<BusinessUnit> BusinessUnits => Set<BusinessUnit>();
    public DbSet<Sku> Skus => Set<Sku>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<Program> Programs => Set<Program>();
    public DbSet<Block> Blocks => Set<Block>();
    public DbSet<Lot> Lots => Set<Lot>();
    public DbSet<ControlPoint> ControlPoints => Set<ControlPoint>();
    public DbSet<RfidReader> RfidReaders => Set<RfidReader>();
    public DbSet<RfidPiece> RfidPieces => Set<RfidPiece>();
    public DbSet<PieceControlPointAdvanceReading> PieceControlPointAdvanceReadings => Set<PieceControlPointAdvanceReading>();
    public DbSet<ProgramGoal> ProgramGoals => Set<ProgramGoal>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Module> Modules => Set<Module>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

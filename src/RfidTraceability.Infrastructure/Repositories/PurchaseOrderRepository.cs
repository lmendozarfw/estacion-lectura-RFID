using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class PurchaseOrderRepository(AppDbContext context) : IPurchaseOrderRepository
{
    public async Task<PurchaseOrder> AddAsync(PurchaseOrder entity, CancellationToken cancellationToken = default)
    {
        var value = await context.PurchaseOrders.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var purchaseOrder = await context.PurchaseOrders.AsQueryable().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        return purchaseOrder != null;
    }

    public async Task<bool> ExistsByCodeAsync(Guid organizationId, string code, CancellationToken cancellationToken = default)
    {
        var purchaseOrder = await context.PurchaseOrders.AsQueryable().AsNoTracking().FirstOrDefaultAsync(p => p.OrganizationId == organizationId && p.Code == code, cancellationToken);
        return purchaseOrder != null;
    }

    public async Task<IReadOnlyList<PurchaseOrder>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.PurchaseOrders.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<PurchaseOrder?> GetByCodeAsync(Guid organizationId, string code, CancellationToken cancellationToken = default)
    {
        return await context.PurchaseOrders.AsQueryable().AsNoTracking().FirstOrDefaultAsync(p => p.OrganizationId == organizationId && p.Code == code, cancellationToken);
    }

    public async Task<PurchaseOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PurchaseOrders.AsQueryable().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PurchaseOrder?> GetWithProgramsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PurchaseOrders.AsQueryable().AsNoTracking().Include(p => p.Programs).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PurchaseOrder>> ListByOrganizationAsync(Guid organizationId, CancellationToken cancellationToken = default)
    {
        return await context.PurchaseOrders.AsQueryable().AsNoTracking().Where(p => p.OrganizationId == organizationId).ToListAsync(cancellationToken);
    }

    public void Remove(PurchaseOrder entity)
    {
        context.PurchaseOrders.Remove(entity);
    }

    public void Update(PurchaseOrder entity)
    {
        context.PurchaseOrders.Update(entity);
    }
}

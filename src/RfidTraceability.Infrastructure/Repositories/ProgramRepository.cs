using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class ProgramRepository(AppDbContext context) : IProgramRepository
{
    public async Task<Program> AddAsync(Program entity, CancellationToken cancellationToken = default)
    {
        var value = await context.Programs.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var program = await context.Programs.AsQueryable().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        return program != null;
    }

    public async Task<bool> ExistsByCodeAsync(Guid organizationId, string code, CancellationToken cancellationToken = default)
    {
        return await context.Programs.AsQueryable().AsNoTracking()
            .AnyAsync(p => p.PurchaseOrder.OrganizationId == organizationId && p.Code == code, cancellationToken);
    }

    public async Task<IReadOnlyList<Program>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Programs.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Program?> GetByCodeAsync(Guid purchaseOrderId, string code, CancellationToken cancellationToken = default)
    {
        return await context.Programs.AsQueryable().AsNoTracking().FirstOrDefaultAsync(p => p.PurchaseOrderId == purchaseOrderId && p.Code == code, cancellationToken);
    }

    public async Task<Program?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Programs.AsQueryable().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Program?> GetWithBlocksAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Programs.AsQueryable().AsNoTracking().Include(p => p.Blocks).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Program?> GetWithGoalsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Programs.AsQueryable().AsNoTracking().Include(p => p.Goals).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Program>> ListByPurchaseOrderAsync(Guid purchaseOrderId, CancellationToken cancellationToken = default)
    {
        return await context.Programs.AsQueryable().AsNoTracking().Where(p => p.PurchaseOrderId == purchaseOrderId).ToListAsync(cancellationToken);
    }

    public void Remove(Program entity)
    {
        context.Programs.Remove(entity);
    }

    public void Update(Program entity)
    {
        context.Programs.Update(entity);
    }
}

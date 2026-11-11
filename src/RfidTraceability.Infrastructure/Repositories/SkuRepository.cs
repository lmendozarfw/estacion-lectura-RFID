using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class SkuRepository(AppDbContext context) : ISkuRepository
{
    public async Task<Sku> AddAsync(Sku entity, CancellationToken cancellationToken = default)
    {
        var value = await context.Skus.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sku = await context.Skus.AsQueryable().AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return sku != null;
    }

    public async Task<bool> ExistsByCodeAsync(Guid organizationId, string code, CancellationToken cancellationToken = default)
    {
        var sku = await context.Skus.AsQueryable().AsNoTracking().FirstOrDefaultAsync(s => s.OrganizationId == organizationId && s.Code == code, cancellationToken);
        return sku != null;
    }

    public async Task<IReadOnlyList<Sku>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Skus.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Sku?> GetByCodeAsync(Guid organizationId, string code, CancellationToken cancellationToken = default)
    {
        return await context.Skus.AsQueryable().AsNoTracking().FirstOrDefaultAsync(s => s.OrganizationId == organizationId && s.Code == code, cancellationToken);
    }

    public async Task<Sku?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Skus.AsQueryable().AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Sku>> ListByOrganizationAsync(Guid organizationId, CancellationToken cancellationToken = default)
    {
        return await context.Skus.AsQueryable().AsNoTracking().Where(s => s.OrganizationId == organizationId).ToListAsync(cancellationToken);
    }

    public void Remove(Sku entity)
    {
        context.Skus.Remove(entity);
    }

    public void Update(Sku entity)
    {
        context.Skus.Update(entity);
    }
}

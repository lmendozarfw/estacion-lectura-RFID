using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class OrganizationRepository(AppDbContext context) : IOrganizationRepository
{
    public async Task<Organization> AddAsync(Organization entity, CancellationToken cancellationToken = default)
    {
        var value = await context.Organizations.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var organization = await context.Organizations.AsQueryable().AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        return organization != null;
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var organization = await context.Organizations.AsQueryable().AsNoTracking().FirstOrDefaultAsync(o => o.Code == code, cancellationToken);
        return organization != null;
    }

    public async Task<IReadOnlyList<Organization>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Organizations.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Organization?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await context.Organizations.AsQueryable().AsNoTracking().FirstOrDefaultAsync(o => o.Code == code, cancellationToken);
    }

    public async Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Organizations.AsQueryable().AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<Organization?> GetWithBusinessUnitsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Organizations.AsQueryable().AsNoTracking().Include(o => o.BusinessUnits).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public void Remove(Organization entity)
    {
        context.Organizations.Remove(entity);
    }

    public void Update(Organization entity)
    {
        context.Organizations.Update(entity);
    }
}

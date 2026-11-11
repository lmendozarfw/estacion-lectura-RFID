using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class BusinessUnitRepository(AppDbContext context) : IBusinessUnitRepository
{
    public async Task<BusinessUnit> AddAsync(BusinessUnit entity, CancellationToken cancellationToken = default)
    {
        var value = await context.BusinessUnits.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var businessUnit = await context.BusinessUnits.AsQueryable().AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        return businessUnit != null;
    }

    public async Task<IReadOnlyList<BusinessUnit>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.BusinessUnits.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<BusinessUnit?> GetByCodeAsync(Guid organizationId, string code, CancellationToken cancellationToken = default)
    {
        return await context.BusinessUnits.AsQueryable().AsNoTracking().FirstOrDefaultAsync(b => b.OrganizationId == organizationId && b.Code == code, cancellationToken);
    }

    public async Task<BusinessUnit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.BusinessUnits.AsQueryable().AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BusinessUnit>> ListByOrganizationAsync(Guid organizationId, CancellationToken cancellationToken = default)
    {
        return await context.BusinessUnits.AsQueryable().AsNoTracking().Where(b => b.OrganizationId == organizationId).ToListAsync(cancellationToken);
    }

    public void Remove(BusinessUnit entity)
    {
        context.BusinessUnits.Remove(entity);
    }

    public void Update(BusinessUnit entity)
    {
        context.BusinessUnits.Update(entity);
    }
}

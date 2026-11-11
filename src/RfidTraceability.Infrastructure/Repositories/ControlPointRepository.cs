using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class ControlPointRepository(AppDbContext context) : IControlPointRepository
{
    public async Task<ControlPoint> AddAsync(ControlPoint entity, CancellationToken cancellationToken = default)
    {
        var value = await context.ControlPoints.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var controlPoint = await context.ControlPoints.AsQueryable().AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return controlPoint != null;
    }

    public async Task<IReadOnlyList<ControlPoint>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.ControlPoints.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<ControlPoint?> GetByCodeAsync(Guid businessUnitId, string code, CancellationToken cancellationToken = default)
    {
        return await context.ControlPoints.AsQueryable().AsNoTracking().FirstOrDefaultAsync(c => c.BusinessUnitId == businessUnitId && c.Code == code, cancellationToken);
    }

    public async Task<ControlPoint?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ControlPoints.AsQueryable().AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ControlPoint>> ListByBusinessUnitAsync(Guid businessUnitId, CancellationToken cancellationToken = default)
    {
        return await context.ControlPoints.AsQueryable().AsNoTracking().Where(c => c.BusinessUnitId == businessUnitId).ToListAsync(cancellationToken);
    }

    public void Remove(ControlPoint entity)
    {
        context.ControlPoints.Remove(entity);
    }

    public void Update(ControlPoint entity)
    {
        context.ControlPoints.Update(entity);
    }
}

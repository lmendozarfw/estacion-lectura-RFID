using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class RfidReaderRepository(AppDbContext context) : IRfidReaderRepository
{
    public async Task<RfidReader> AddAsync(RfidReader entity, CancellationToken cancellationToken = default)
    {
        var value = await context.RfidReaders.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var reader = await context.RfidReaders.AsQueryable().AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        return reader != null;
    }

    public async Task<IReadOnlyList<RfidReader>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.RfidReaders.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<RfidReader?> GetByCodeAsync(Guid controlPointId, string code, CancellationToken cancellationToken = default)
    {
        return await context.RfidReaders.AsQueryable().AsNoTracking().FirstOrDefaultAsync(r => r.ControlPointId == controlPointId && r.Code == code, cancellationToken);
    }

    public async Task<RfidReader?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.RfidReaders.AsQueryable().AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<RfidReader?> GetBySerialNumberAsync(string serialNumber, CancellationToken cancellationToken = default)
    {
        return await context.RfidReaders.AsQueryable().AsNoTracking().FirstOrDefaultAsync(r => r.SerialNumber == serialNumber, cancellationToken);
    }

    public async Task<IReadOnlyList<RfidReader>> ListByControlPointAsync(Guid controlPointId, CancellationToken cancellationToken = default)
    {
        return await context.RfidReaders.AsQueryable().AsNoTracking().Where(r => r.ControlPointId == controlPointId).ToListAsync(cancellationToken);
    }

    public void Remove(RfidReader entity)
    {
        context.RfidReaders.Remove(entity);
    }

    public void Update(RfidReader entity)
    {
        context.RfidReaders.Update(entity);
    }
}

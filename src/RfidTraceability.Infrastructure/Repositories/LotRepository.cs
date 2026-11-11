using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class LotRepository(AppDbContext context) : ILotRepository
{
    public async Task<Lot> AddAsync(Lot entity, CancellationToken cancellationToken = default)
    {
        var value = await context.Lots.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var lot = await context.Lots.AsQueryable().AsNoTracking().FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        return lot != null;
    }

    public async Task<IReadOnlyList<Lot>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Lots.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Lot?> GetByCodeAsync(Guid blockId, string code, CancellationToken cancellationToken = default)
    {
        return await context.Lots.AsQueryable().AsNoTracking().FirstOrDefaultAsync(l => l.BlockId == blockId && l.Code == code, cancellationToken);
    }

    public async Task<Lot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Lots.AsQueryable().AsNoTracking().FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<Lot?> GetWithPiecesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Lots.AsQueryable().AsNoTracking().Include(l => l.RfidPieces).FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Lot>> ListByBlockAsync(Guid blockId, CancellationToken cancellationToken = default)
    {
        return await context.Lots.AsQueryable().AsNoTracking().Where(l => l.BlockId == blockId).ToListAsync(cancellationToken);
    }

    public void Remove(Lot entity)
    {
        context.Lots.Remove(entity);
    }

    public void Update(Lot entity)
    {
        context.Lots.Update(entity);
    }
}

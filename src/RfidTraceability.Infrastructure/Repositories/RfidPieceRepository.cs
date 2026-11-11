using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class RfidPieceRepository(AppDbContext context) : IRfidPieceRepository
{
    public async Task<RfidPiece> AddAsync(RfidPiece entity, CancellationToken cancellationToken = default)
    {
        var value = await context.RfidPieces.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<int> CountByLotAsync(Guid lotId, CancellationToken cancellationToken = default)
    {
        return await context.RfidPieces.AsQueryable().AsNoTracking().CountAsync(p => p.LotId == lotId, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var piece = await context.RfidPieces.AsQueryable().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        return piece != null;
    }

    public async Task<bool> ExistsByEpcAsync(string epc, CancellationToken cancellationToken = default)
    {
        var piece = await context.RfidPieces.AsQueryable().AsNoTracking().FirstOrDefaultAsync(p => p.Epc == epc, cancellationToken);
        return piece != null;
    }

    public async Task<IReadOnlyList<RfidPiece>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.RfidPieces.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<RfidPiece?> GetByEpcAsync(string epc, CancellationToken cancellationToken = default)
    {
        return await context.RfidPieces.AsQueryable().AsNoTracking().FirstOrDefaultAsync(p => p.Epc == epc, cancellationToken);
    }

    public async Task<RfidPiece?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.RfidPieces.AsQueryable().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<RfidPiece?> GetWithTraceabilityAsync(string epc, CancellationToken cancellationToken = default)
    {
        return await context.RfidPieces.AsQueryable().AsNoTracking()
            .Include(p => p.Sku)
            .Include(p => p.Lot)
                .ThenInclude(l => l.Block)
                .ThenInclude(b => b.Program)
            .Include(p => p.AdvanceReadings)
                .ThenInclude(r => r.ControlPoint)
            .FirstOrDefaultAsync(p => p.Epc == epc, cancellationToken);
    }

    public async Task<IReadOnlyList<RfidPiece>> ListByLotAsync(Guid lotId, CancellationToken cancellationToken = default)
    {
        return await context.RfidPieces.AsQueryable().AsNoTracking().Where(p => p.LotId == lotId).ToListAsync(cancellationToken);
    }

    public void Remove(RfidPiece entity)
    {
        context.RfidPieces.Remove(entity);
    }

    public void Update(RfidPiece entity)
    {
        context.RfidPieces.Update(entity);
    }
}

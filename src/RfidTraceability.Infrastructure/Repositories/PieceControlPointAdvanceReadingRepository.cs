using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class PieceControlPointAdvanceReadingRepository(AppDbContext context) : IPieceControlPointAdvanceReadingRepository
{
    public async Task<PieceControlPointAdvanceReading> AddAsync(PieceControlPointAdvanceReading entity, CancellationToken cancellationToken = default)
    {
        var value = await context.PieceControlPointAdvanceReadings.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<int> CountDistinctPiecesByProgramAndControlPointAndDateAsync(
        Guid programId,
        Guid controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        var (start, end) = ToUtcDayRange(date);

        return await context.PieceControlPointAdvanceReadings.AsQueryable().AsNoTracking()
            .Where(r => r.RfidPiece.Lot.Block.ProgramId == programId && r.ControlPointId == controlPointId && r.Date >= start && r.Date < end)
            .Select(r => r.RfidPieceId)
            .Distinct()
            .CountAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var reading = await context.PieceControlPointAdvanceReadings.AsQueryable().AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        return reading != null;
    }

    public async Task<bool> ExistsBySourceEventIdAsync(Guid sourceEventId, CancellationToken cancellationToken = default)
    {
        var reading = await context.PieceControlPointAdvanceReadings.AsQueryable().AsNoTracking().FirstOrDefaultAsync(r => r.SourceEventId == sourceEventId, cancellationToken);
        return reading != null;
    }

    public async Task<IReadOnlyList<PieceControlPointAdvanceReading>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.PieceControlPointAdvanceReadings.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<PieceControlPointAdvanceReading?> GetBySourceEventIdAsync(Guid sourceEventId, CancellationToken cancellationToken = default)
    {
        return await context.PieceControlPointAdvanceReadings.AsQueryable().AsNoTracking().FirstOrDefaultAsync(r => r.SourceEventId == sourceEventId, cancellationToken);
    }

    public async Task<PieceControlPointAdvanceReading?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PieceControlPointAdvanceReadings.AsQueryable().AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PieceControlPointAdvanceReading>> ListByPieceAndControlPointAsync(
        Guid rfidPieceId,
        Guid controlPointId,
        CancellationToken cancellationToken = default)
    {
        return await context.PieceControlPointAdvanceReadings.AsQueryable().AsNoTracking()
            .Where(r => r.RfidPieceId == rfidPieceId && r.ControlPointId == controlPointId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PieceControlPointAdvanceReading>> ListByPieceAsync(
        Guid rfidPieceId,
        CancellationToken cancellationToken = default)
    {
        return await context.PieceControlPointAdvanceReadings.AsQueryable().AsNoTracking()
            .Where(r => r.RfidPieceId == rfidPieceId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PieceControlPointAdvanceReading>> ListByProgramAndControlPointAndDateAsync(
        Guid programId,
        Guid controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        var (start, end) = ToUtcDayRange(date);

        return await context.PieceControlPointAdvanceReadings.AsQueryable().AsNoTracking()
            .Where(r => r.RfidPiece.Lot.Block.ProgramId == programId && r.ControlPointId == controlPointId && r.Date >= start && r.Date < end)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PieceControlPointAdvanceReading>> ListByProgramAndDateAsync(
        Guid programId,
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        var (start, end) = ToUtcDayRange(date);

        return await context.PieceControlPointAdvanceReadings.AsQueryable().AsNoTracking()
            .Where(r => r.RfidPiece.Lot.Block.ProgramId == programId && r.Date >= start && r.Date < end)
            .ToListAsync(cancellationToken);
    }

    public void Remove(PieceControlPointAdvanceReading entity)
    {
        context.PieceControlPointAdvanceReadings.Remove(entity);
    }

    public void Update(PieceControlPointAdvanceReading entity)
    {
        context.PieceControlPointAdvanceReadings.Update(entity);
    }

    private static (DateTime Start, DateTime End) ToUtcDayRange(DateOnly date)
    {
        var start = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        return (start, start.AddDays(1));
    }
}

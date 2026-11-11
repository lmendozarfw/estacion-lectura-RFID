using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class ProgramGoalRepository(AppDbContext context) : IProgramGoalRepository
{
    public async Task<ProgramGoal> AddAsync(ProgramGoal entity, CancellationToken cancellationToken = default)
    {
        var value = await context.ProgramGoals.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var goal = await context.ProgramGoals.AsQueryable().AsNoTracking().FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
        return goal != null;
    }

    public async Task<bool> ExistsForProgramControlPointAndDateAsync(
        Guid programId,
        Guid controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        var goal = await context.ProgramGoals.AsQueryable().AsNoTracking()
            .FirstOrDefaultAsync(g => g.ProgramId == programId && g.ControlPointId == controlPointId && g.Date == date, cancellationToken);
        return goal != null;
    }

    public async Task<IReadOnlyList<ProgramGoal>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.ProgramGoals.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<ProgramGoal?> GetByProgramControlPointAndDateAsync(
        Guid programId,
        Guid controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        return await context.ProgramGoals.AsQueryable().AsNoTracking()
            .FirstOrDefaultAsync(g => g.ProgramId == programId && g.ControlPointId == controlPointId && g.Date == date, cancellationToken);
    }

    public async Task<ProgramGoal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ProgramGoals.AsQueryable().AsNoTracking().FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProgramGoal>> ListByProgramAsync(
        Guid programId,
        CancellationToken cancellationToken = default)
    {
        return await context.ProgramGoals.AsQueryable().AsNoTracking().Where(g => g.ProgramId == programId).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProgramGoal>> ListByProgramAndControlPointAsync(
        Guid programId,
        Guid controlPointId,
        CancellationToken cancellationToken = default)
    {
        return await context.ProgramGoals.AsQueryable().AsNoTracking()
            .Where(g => g.ProgramId == programId && g.ControlPointId == controlPointId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProgramGoal>> ListByProgramAndDateAsync(
        Guid programId,
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        return await context.ProgramGoals.AsQueryable().AsNoTracking()
            .Where(g => g.ProgramId == programId && g.Date == date)
            .ToListAsync(cancellationToken);
    }

    public void Remove(ProgramGoal entity)
    {
        context.ProgramGoals.Remove(entity);
    }

    public void Update(ProgramGoal entity)
    {
        context.ProgramGoals.Update(entity);
    }
}

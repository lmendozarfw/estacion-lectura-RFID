using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IProgramGoalRepository : IRepository<ProgramGoal>
{
    Task<ProgramGoal?> GetByProgramControlPointAndDateAsync(
        Guid programId,
        Guid controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsForProgramControlPointAndDateAsync(
        Guid programId,
        Guid controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProgramGoal>> ListByProgramAsync(
        Guid programId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProgramGoal>> ListByProgramAndDateAsync(
        Guid programId,
        DateOnly date,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProgramGoal>> ListByProgramAndControlPointAsync(
        Guid programId,
        Guid controlPointId,
        CancellationToken cancellationToken = default);
}

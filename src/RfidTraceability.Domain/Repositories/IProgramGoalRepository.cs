using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IProgramGoalRepository : IRepository<ProgramGoal>
{
    Task<ProgramGoal?> GetByProgramControlPointAndDateAsync(
        string programId,
        string controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsForProgramControlPointAndDateAsync(
        string programId,
        string controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProgramGoal>> ListByProgramAsync(
        string programId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProgramGoal>> ListByProgramAndDateAsync(
        string programId,
        DateOnly date,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProgramGoal>> ListByProgramAndControlPointAsync(
        string programId,
        string controlPointId,
        CancellationToken cancellationToken = default);
}

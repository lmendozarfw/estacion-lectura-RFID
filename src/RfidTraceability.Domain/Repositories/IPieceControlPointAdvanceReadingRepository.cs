using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IPieceControlPointAdvanceReadingRepository : IRepository<PieceControlPointAdvanceReading>
{
    Task<PieceControlPointAdvanceReading?> GetBySourceEventIdAsync(
        Guid sourceEventId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsBySourceEventIdAsync(
        Guid sourceEventId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PieceControlPointAdvanceReading>> ListByPieceAndControlPointAsync(
        Guid rfidPieceId,
        Guid controlPointId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PieceControlPointAdvanceReading>> ListByPieceAsync(
        Guid rfidPieceId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PieceControlPointAdvanceReading>> ListByProgramAndDateAsync(
        Guid programId,
        DateOnly date,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PieceControlPointAdvanceReading>> ListByProgramAndControlPointAndDateAsync(
        Guid programId,
        Guid controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default);

    Task<int> CountDistinctPiecesByProgramAndControlPointAndDateAsync(
        Guid programId,
        Guid controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default);
}

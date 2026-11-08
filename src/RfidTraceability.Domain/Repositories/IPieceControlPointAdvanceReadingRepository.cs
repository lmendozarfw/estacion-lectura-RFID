using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IPieceControlPointAdvanceReadingRepository : IRepository<PieceControlPointAdvanceReading>
{
    Task<PieceControlPointAdvanceReading?> GetByPieceAndControlPointAsync(
        string rfidPieceId,
        string controlPointId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsForPieceAndControlPointAsync(
        string rfidPieceId,
        string controlPointId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PieceControlPointAdvanceReading>> ListByPieceAsync(
        string rfidPieceId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PieceControlPointAdvanceReading>> ListByProgramAndDateAsync(
        string programId,
        DateOnly date,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PieceControlPointAdvanceReading>> ListByProgramAndControlPointAndDateAsync(
        string programId,
        string controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default);

    Task<int> CountDistinctPiecesByProgramAndControlPointAndDateAsync(
        string programId,
        string controlPointId,
        DateOnly date,
        CancellationToken cancellationToken = default);
}

using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IRfidPieceRepository : IRepository<RfidPiece>
{
    Task<RfidPiece?> GetByEpcAsync(string epc, CancellationToken cancellationToken = default);

    Task<bool> ExistsByEpcAsync(string epc, CancellationToken cancellationToken = default);

    Task<RfidPiece?> GetWithTraceabilityAsync(string epc, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<RfidPiece>> ListByLotAsync(string lotId, CancellationToken cancellationToken = default);

    Task<int> CountByLotAsync(string lotId, CancellationToken cancellationToken = default);
}

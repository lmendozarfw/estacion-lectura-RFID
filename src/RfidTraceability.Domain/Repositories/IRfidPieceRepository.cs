using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IRfidPieceRepository : IRepository<RfidPiece>
{
    Task<RfidPiece?> GetByEpcAsync(string epc, CancellationToken cancellationToken = default);

    Task<bool> ExistsByEpcAsync(string epc, CancellationToken cancellationToken = default);

    Task<RfidPiece?> GetWithTraceabilityAsync(string epc, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<RfidPiece>> ListByLotAsync(Guid lotId, CancellationToken cancellationToken = default);

    Task<int> CountByLotAsync(Guid lotId, CancellationToken cancellationToken = default);
}

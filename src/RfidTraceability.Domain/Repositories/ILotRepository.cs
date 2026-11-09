using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface ILotRepository : IRepository<Lot>
{
    Task<Lot?> GetByCodeAsync(string blockId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Lot>> ListByBlockAsync(string blockId, CancellationToken cancellationToken = default);

    Task<Lot?> GetWithPiecesAsync(string id, CancellationToken cancellationToken = default);
}

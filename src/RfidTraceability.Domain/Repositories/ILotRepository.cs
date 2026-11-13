using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface ILotRepository : IRepository<Lot>
{
    Task<Lot?> GetByCodeAsync(Guid blockId, string code, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(Guid blockId, string code, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Lot>> ListByBlockAsync(Guid blockId, CancellationToken cancellationToken = default);

    Task<Lot?> GetWithPiecesAsync(Guid id, CancellationToken cancellationToken = default);
}

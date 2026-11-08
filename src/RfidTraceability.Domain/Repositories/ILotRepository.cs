using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface ILotRepository : IRepository<Lot>
{
    Task<Lot?> GetByCodeAsync(string programId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Lot>> ListByProgramAsync(string programId, CancellationToken cancellationToken = default);

    Task<Lot?> GetWithPiecesAsync(string id, CancellationToken cancellationToken = default);
}

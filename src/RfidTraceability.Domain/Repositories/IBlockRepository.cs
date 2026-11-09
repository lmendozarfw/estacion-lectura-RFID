using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IBlockRepository : IRepository<Block>
{
    Task<Block?> GetByCodeAsync(string programId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Block>> ListByProgramAsync(string programId, CancellationToken cancellationToken = default);

    Task<Block?> GetWithLotsAsync(string id, CancellationToken cancellationToken = default);
}

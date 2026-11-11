using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IBlockRepository : IRepository<Block>
{
    Task<Block?> GetByCodeAsync(Guid programId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Block>> ListByProgramAsync(Guid programId, CancellationToken cancellationToken = default);

    Task<Block?> GetWithLotsAsync(Guid id, CancellationToken cancellationToken = default);
}

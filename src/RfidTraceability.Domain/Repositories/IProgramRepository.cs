using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IProgramRepository : IRepository<Program>
{
    Task<Program?> GetByCodeAsync(Guid purchaseOrderId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Program>> ListByPurchaseOrderAsync(Guid purchaseOrderId, CancellationToken cancellationToken = default);

    Task<Program?> GetWithBlocksAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Program?> GetWithGoalsAsync(Guid id, CancellationToken cancellationToken = default);
}

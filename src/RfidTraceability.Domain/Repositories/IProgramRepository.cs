using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IProgramRepository : IRepository<Program>
{
    Task<Program?> GetByCodeAsync(string purchaseOrderId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Program>> ListByPurchaseOrderAsync(string purchaseOrderId, CancellationToken cancellationToken = default);

    Task<Program?> GetWithBlocksAsync(string id, CancellationToken cancellationToken = default);

    Task<Program?> GetWithGoalsAsync(string id, CancellationToken cancellationToken = default);
}

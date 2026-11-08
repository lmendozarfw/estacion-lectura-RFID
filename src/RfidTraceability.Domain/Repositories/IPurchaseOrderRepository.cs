using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
{
    Task<PurchaseOrder?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<PurchaseOrder?> GetWithProgramsAsync(string id, CancellationToken cancellationToken = default);
}

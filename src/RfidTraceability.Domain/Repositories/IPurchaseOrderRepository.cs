using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
{
    Task<PurchaseOrder?> GetByCodeAsync(Guid organizationId, string code, CancellationToken cancellationToken = default);

    Task<bool> ExistsByCodeAsync(Guid organizationId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PurchaseOrder>> ListByOrganizationAsync(Guid organizationId, CancellationToken cancellationToken = default);

    Task<PurchaseOrder?> GetWithProgramsAsync(Guid id, CancellationToken cancellationToken = default);
}

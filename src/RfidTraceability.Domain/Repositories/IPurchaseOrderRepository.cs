using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
{
    Task<PurchaseOrder?> GetByCodeAsync(string organizationId, string code, CancellationToken cancellationToken = default);

    Task<bool> ExistsByCodeAsync(string organizationId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PurchaseOrder>> ListByOrganizationAsync(string organizationId, CancellationToken cancellationToken = default);

    Task<PurchaseOrder?> GetWithProgramsAsync(string id, CancellationToken cancellationToken = default);
}

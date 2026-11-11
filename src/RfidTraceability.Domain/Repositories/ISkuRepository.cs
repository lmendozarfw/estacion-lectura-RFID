using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface ISkuRepository : IRepository<Sku>
{
    Task<Sku?> GetByCodeAsync(Guid organizationId, string code, CancellationToken cancellationToken = default);

    Task<bool> ExistsByCodeAsync(Guid organizationId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Sku>> ListByOrganizationAsync(Guid organizationId, CancellationToken cancellationToken = default);
}

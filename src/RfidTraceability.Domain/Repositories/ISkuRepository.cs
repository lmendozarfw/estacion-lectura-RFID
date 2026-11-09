using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface ISkuRepository : IRepository<Sku>
{
    Task<Sku?> GetByCodeAsync(string organizationId, string code, CancellationToken cancellationToken = default);

    Task<bool> ExistsByCodeAsync(string organizationId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Sku>> ListByOrganizationAsync(string organizationId, CancellationToken cancellationToken = default);
}

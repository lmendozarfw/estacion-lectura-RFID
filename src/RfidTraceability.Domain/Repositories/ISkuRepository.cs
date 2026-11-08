using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface ISkuRepository : IRepository<Sku>
{
    Task<Sku?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}

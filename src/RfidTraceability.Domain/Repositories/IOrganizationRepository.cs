using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IOrganizationRepository : IRepository<Organization>
{
    Task<Organization?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<Organization?> GetWithBusinessUnitsAsync(string id, CancellationToken cancellationToken = default);
}

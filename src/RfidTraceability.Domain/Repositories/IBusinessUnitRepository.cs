using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IBusinessUnitRepository : IRepository<BusinessUnit>
{
    Task<BusinessUnit?> GetByCodeAsync(string organizationId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BusinessUnit>> ListByOrganizationAsync(string organizationId, CancellationToken cancellationToken = default);
}

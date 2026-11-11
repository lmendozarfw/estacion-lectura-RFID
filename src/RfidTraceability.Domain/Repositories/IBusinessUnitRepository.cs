using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IBusinessUnitRepository : IRepository<BusinessUnit>
{
    Task<BusinessUnit?> GetByCodeAsync(Guid organizationId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BusinessUnit>> ListByOrganizationAsync(Guid organizationId, CancellationToken cancellationToken = default);
}

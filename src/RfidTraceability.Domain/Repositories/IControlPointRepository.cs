using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IControlPointRepository : IRepository<ControlPoint>
{
    Task<ControlPoint?> GetByCodeAsync(Guid businessUnitId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ControlPoint>> ListByBusinessUnitAsync(Guid businessUnitId, CancellationToken cancellationToken = default);
}

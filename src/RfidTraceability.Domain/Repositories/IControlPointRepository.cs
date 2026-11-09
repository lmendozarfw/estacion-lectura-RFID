using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IControlPointRepository : IRepository<ControlPoint>
{
    Task<ControlPoint?> GetByCodeAsync(string businessUnitId, string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ControlPoint>> ListByBusinessUnitAsync(string businessUnitId, CancellationToken cancellationToken = default);
}

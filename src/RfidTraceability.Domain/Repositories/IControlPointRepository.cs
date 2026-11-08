using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IControlPointRepository : IRepository<ControlPoint>
{
    Task<ControlPoint?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}

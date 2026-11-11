using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IRfidReaderRepository : IRepository<RfidReader>
{
    Task<RfidReader?> GetByCodeAsync(Guid controlPointId, string code, CancellationToken cancellationToken = default);

    Task<RfidReader?> GetBySerialNumberAsync(string serialNumber, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<RfidReader>> ListByControlPointAsync(Guid controlPointId, CancellationToken cancellationToken = default);
}

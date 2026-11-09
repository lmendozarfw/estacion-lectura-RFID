using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IRfidReaderRepository : IRepository<RfidReader>
{
    Task<RfidReader?> GetByCodeAsync(string controlPointId, string code, CancellationToken cancellationToken = default);

    Task<RfidReader?> GetBySerialNumberAsync(string serialNumber, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<RfidReader>> ListByControlPointAsync(string controlPointId, CancellationToken cancellationToken = default);
}

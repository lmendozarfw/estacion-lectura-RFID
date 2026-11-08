namespace RfidTraceability.Application.Readings;

public interface IEpcReadingProcessor
{
    Task ProcessAsync(EpcReading reading, CancellationToken cancellationToken = default);
}

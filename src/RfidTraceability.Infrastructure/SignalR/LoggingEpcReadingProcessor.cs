using Microsoft.Extensions.Logging;
using RfidTraceability.Application.Readings;

namespace RfidTraceability.Infrastructure.SignalR;

public sealed class LoggingEpcReadingProcessor : IEpcReadingProcessor
{
    private readonly ILogger<LoggingEpcReadingProcessor> _logger;

    public LoggingEpcReadingProcessor(ILogger<LoggingEpcReadingProcessor> logger)
    {
        _logger = logger;
    }

    public Task ProcessAsync(EpcReading reading, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "EPC received: {Epc} at {ReceivedAt}",
            reading.Epc,
            reading.ReceivedAt);

        return Task.CompletedTask;
    }
}

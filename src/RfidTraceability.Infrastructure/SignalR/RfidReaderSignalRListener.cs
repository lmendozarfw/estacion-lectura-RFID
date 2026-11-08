using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RfidTraceability.Application.Readings;

namespace RfidTraceability.Infrastructure.SignalR;

public sealed class RfidReaderSignalRListener : BackgroundService
{
    private readonly RfidReaderHubOptions _options;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<RfidReaderSignalRListener> _logger;

    public RfidReaderSignalRListener(
        IOptions<RfidReaderHubOptions> options,
        IServiceScopeFactory scopeFactory,
        ILogger<RfidReaderSignalRListener> logger)
    {
        _options = options.Value;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(_options.HubUrl))
        {
            _logger.LogWarning("RfidReader HubUrl is not configured. SignalR listener is disabled.");
            return;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            await using var connection = BuildConnection();

            try
            {
                await connection.StartAsync(stoppingToken);
                _logger.LogInformation("Connected to RFID reader hub at {HubUrl}", _options.HubUrl);

                await WaitForDisconnectionAsync(connection, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while connected to RFID reader hub at {HubUrl}", _options.HubUrl);
            }

            if (stoppingToken.IsCancellationRequested)
            {
                break;
            }

            var delay = TimeSpan.FromSeconds(Math.Max(1, _options.ReconnectDelaySeconds));
            _logger.LogInformation("Retrying RFID reader hub connection in {DelaySeconds} seconds", delay.TotalSeconds);

            try
            {
                await Task.Delay(delay, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    private HubConnection BuildConnection()
    {
        var connection = new HubConnectionBuilder()
            .WithUrl(_options.HubUrl)
            .WithAutomaticReconnect()
            .Build();

        connection.On<EpcReaderMessage>(_options.ReadingEvent, HandleReadingAsync);

        connection.Reconnecting += exception =>
        {
            _logger.LogWarning(exception, "RFID reader hub connection lost. Reconnecting...");
            return Task.CompletedTask;
        };

        connection.Reconnected += connectionId =>
        {
            _logger.LogInformation("Reconnected to RFID reader hub. ConnectionId: {ConnectionId}", connectionId);
            return Task.CompletedTask;
        };

        connection.Closed += exception =>
        {
            _logger.LogWarning(exception, "RFID reader hub connection closed.");
            return Task.CompletedTask;
        };

        return connection;
    }

    private async Task HandleReadingAsync(EpcReaderMessage message)
    {
        if (string.IsNullOrWhiteSpace(message.Epc))
        {
            _logger.LogWarning("Received an empty EPC from RFID reader hub.");
            return;
        }

        var reading = new EpcReading(message.Epc, DateTimeOffset.UtcNow);

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var processor = scope.ServiceProvider.GetRequiredService<IEpcReadingProcessor>();
            await processor.ProcessAsync(reading, CancellationToken.None);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error processing EPC {Epc}", message.Epc);
        }
    }

    private static async Task WaitForDisconnectionAsync(HubConnection connection, CancellationToken cancellationToken)
    {
        var completionSource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        Task OnClosed(Exception? exception)
        {
            completionSource.TrySetResult();
            return Task.CompletedTask;
        }

        connection.Closed += OnClosed;

        try
        {
            await completionSource.Task.WaitAsync(cancellationToken);
        }
        finally
        {
            connection.Closed -= OnClosed;
        }
    }
}

namespace RfidTraceability.Infrastructure.SignalR;

public sealed class RfidReaderHubOptions
{
    public const string SectionName = "RfidReader";

    public string HubUrl { get; set; } = string.Empty;
    public string ReadingEvent { get; set; } = "epc-reader";
    public int ReconnectDelaySeconds { get; set; } = 5;
}

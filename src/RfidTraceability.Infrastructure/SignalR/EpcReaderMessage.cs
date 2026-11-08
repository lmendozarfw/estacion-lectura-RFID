using System.Text.Json.Serialization;

namespace RfidTraceability.Infrastructure.SignalR;

public sealed class EpcReaderMessage
{
    [JsonPropertyName("epc")]
    public string Epc { get; set; } = string.Empty;
}

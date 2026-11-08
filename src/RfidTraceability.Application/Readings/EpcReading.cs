namespace RfidTraceability.Application.Readings;

public sealed record EpcReading(string Epc, DateTimeOffset ReceivedAt);

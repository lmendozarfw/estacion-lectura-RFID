namespace RfidTraceability.Application.Common.Exceptions;

public sealed class InvalidDateException(string message) : Exception(message);

namespace RfidTraceability.Application.Common.Exceptions;

public sealed class ConflictException(string message) : Exception(message);
namespace RfidTraceability.Application.Common.Exceptions;

public sealed class DuplicateException(string message) : Exception(message);

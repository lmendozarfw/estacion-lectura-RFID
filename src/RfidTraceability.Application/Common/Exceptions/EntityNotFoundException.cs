namespace RfidTraceability.Application.Common.Exceptions;

public sealed class EntityNotFoundException(string entityName, object key)
    : Exception($"No se encontró '{entityName}' con id '{key}'.");

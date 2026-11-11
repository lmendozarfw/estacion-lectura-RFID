using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.Common.Exceptions;

namespace RfidTraceability.Api;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            EntityNotFoundException => (StatusCodes.Status404NotFound, "Recurso no encontrado"),
            DuplicateException => (StatusCodes.Status409Conflict, "Recurso duplicado"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Solicitud inválida"),
            _ => (StatusCodes.Status500InternalServerError, "Error interno del servidor")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Unhandled exception");
        }

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = statusCode == StatusCodes.Status500InternalServerError ? null : exception.Message
        }, cancellationToken);

        return true;
    }
}

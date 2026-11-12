using System.Text.Json;
using RfidTraceability.Application.Common.Exceptions;

namespace RfidTraceability.Api.Middlewares;

public class GlobalExceptionMiddleware
(
    RequestDelegate next, 
    ILogger<GlobalExceptionMiddleware> logger
)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var traceId = context.TraceIdentifier;
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new ErrorResponse()
        {
            TraceId = traceId
        };

        switch (exception)
        {
            case DomainException domainEx:
                response.StatusCode = StatusCodes.Status400BadRequest;
                errorResponse.StatusCode = 400;
                errorResponse.Message = domainEx.Message;
                break;

            case EntityNotFoundException notFoundEx:
                response.StatusCode = StatusCodes.Status404NotFound;
                errorResponse.StatusCode = 404;
                errorResponse.Message = notFoundEx.Message;
                break;

            case ConflictException conflictEx:
                response.StatusCode = StatusCodes.Status409Conflict;
                errorResponse.StatusCode = 409;
                errorResponse.Message = conflictEx.Message;
                break;

            case InvalidDateException invalidDateEx:
                response.StatusCode = StatusCodes.Status409Conflict;
                errorResponse.StatusCode = 409;
                errorResponse.Message = invalidDateEx.Message;
                break;

            case UnauthorizedException unauthorizedEx:
                response.StatusCode = StatusCodes.Status401Unauthorized;
                errorResponse.StatusCode = 401;
                errorResponse.Message = unauthorizedEx.Message;
                break;

            default:
                logger.LogError(exception, "Error no controlado. TraceId: {TraceId}", traceId);
                response.StatusCode = StatusCodes.Status500InternalServerError;
                errorResponse.StatusCode = 500;
                errorResponse.Message = "Ocurrió un error interno en el servidor";
                break;
        }

        var json = JsonSerializer.Serialize(errorResponse);
        await response.WriteAsync(json);
    }
}

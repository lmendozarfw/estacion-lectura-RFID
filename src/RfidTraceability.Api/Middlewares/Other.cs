namespace RfidTraceability.Api.Middlewares;
class OtherMioddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"Desde otro middleware - Request Path: {context.Request.Path}");
        await next(context);
    }
}
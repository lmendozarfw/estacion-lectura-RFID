using RfidTraceability.Api.Middlewares;
using RfidTraceability.Application;
using RfidTraceability.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", policy =>
    {
       policy.AllowAnyHeader();
       policy.AllowAnyMethod();
       policy.AllowAnyOrigin(); 
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Módulo Calidad API");
    });
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseCors("all");
app.MapControllers();
app.MapHub<ProductionHub>("/hubs/production");

app.Run();

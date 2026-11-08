using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RfidTraceability.Application.Readings;
using RfidTraceability.Infrastructure.Persistence;
using RfidTraceability.Infrastructure.SignalR;

namespace RfidTraceability.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.Configure<RfidReaderHubOptions>(
            configuration.GetSection(RfidReaderHubOptions.SectionName));

        services.AddScoped<IEpcReadingProcessor, LoggingEpcReadingProcessor>();
        services.AddHostedService<RfidReaderSignalRListener>();

        return services;
    }
}

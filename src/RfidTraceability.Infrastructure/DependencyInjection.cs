using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Readings;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;
using RfidTraceability.Infrastructure.Repositories;
using RfidTraceability.Infrastructure.SignalR;

namespace RfidTraceability.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        services.Configure<RfidReaderHubOptions>(
            configuration.GetSection(RfidReaderHubOptions.SectionName));

        services.AddScoped<IEpcReadingProcessor, LoggingEpcReadingProcessor>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBlockRepository, BlockRepository>();
        services.AddScoped<IBusinessUnitRepository, BusinessUnitRepository>();
        services.AddScoped<IControlPointRepository, ControlPointRepository>();
        services.AddScoped<ILotRepository, LotRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IPieceControlPointAdvanceReadingRepository, PieceControlPointAdvanceReadingRepository>();
        services.AddScoped<IProgramGoalRepository, ProgramGoalRepository>();
        services.AddScoped<IProgramRepository, ProgramRepository>();
        services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
        services.AddScoped<IRfidPieceRepository, RfidPieceRepository>();
        services.AddScoped<IRfidReaderRepository, RfidReaderRepository>();
        services.AddScoped<ISkuRepository, SkuRepository>();

        services.AddHostedService<RfidReaderSignalRListener>();

        return services;
    }
}

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<ProductionAdvanceService>();
        // prueba
        services.AddSingleton<MiServicio>();
        return services;
    }
}
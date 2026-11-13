using Microsoft.EntityFrameworkCore;
using RfidTraceability.Application.Security;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Api.Seed;

public static class DevelopmentSeeder
{
    public const string DefaultOrganizationCode = "DEMO";
    public const string DefaultUsername = "admin";
    public const string DefaultPassword = "Admin123!";

    public static async Task SeedDevelopmentDataAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
            .CreateLogger(nameof(DevelopmentSeeder));

        var organization = await context.Organizations
            .FirstOrDefaultAsync(o => o.Code == DefaultOrganizationCode);

        if (organization is null)
        {
            organization = new Organization(DefaultOrganizationCode, "Organización demo");
            context.Organizations.Add(organization);
            await context.SaveChangesAsync();
        }

        var userExists = await context.Users
            .AnyAsync(u => u.OrganizationId == organization.Id && u.Username == DefaultUsername);

        if (!userExists)
        {
            var user = new User(
                organization.Id,
                DefaultUsername,
                passwordHasher.Hash(DefaultPassword),
                "EMP-001");

            context.Users.Add(user);
            await context.SaveChangesAsync();

            logger.LogWarning(
                "Usuario demo creado. OrganizationId: {OrganizationId}, Username: {Username}, Password: {Password}",
                organization.Id,
                DefaultUsername,
                DefaultPassword);
        }
    }
}

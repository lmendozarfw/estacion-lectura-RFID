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

        var employee = await context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeNumber == "EMP-001");

        if (employee is null)
        {
            employee = new Employee("EMP-001", "Admin", "Demo", "Demo");
            context.Employees.Add(employee);
            await context.SaveChangesAsync();
        }

        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Username == DefaultUsername);

        if (user is null)
        {
            user = new User(
                organization.Id,
                DefaultUsername,
                passwordHasher.Hash(DefaultPassword),
                employee.Id);

            context.Users.Add(user);
            await context.SaveChangesAsync();

            logger.LogWarning(
                "Usuario demo creado. OrganizationId: {OrganizationId}, Username: {Username}, Password: {Password}",
                organization.Id,
                DefaultUsername,
                DefaultPassword);
        }
        else if (user.EmployeeId is null)
        {
            user.EmployeeId = employee.Id;
            await context.SaveChangesAsync();
        }
    }
}

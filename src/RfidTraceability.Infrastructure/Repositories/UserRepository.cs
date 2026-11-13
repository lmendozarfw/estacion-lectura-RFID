using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User> AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        var value = await context.Users.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.AsQueryable().AsNoTracking().AnyAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(Guid organizationId, string username, CancellationToken cancellationToken = default)
    {
        return await context.Users.AsQueryable().AsNoTracking()
            .AnyAsync(u => u.OrganizationId == organizationId && u.Username == username, cancellationToken);
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Users.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.AsQueryable().AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(Guid organizationId, string username, CancellationToken cancellationToken = default)
    {
        return await context.Users.AsQueryable().AsNoTracking()
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.OrganizationId == organizationId && u.Username == username, cancellationToken);
    }

    public async Task<User?> GetWithOrganizationAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.AsQueryable().AsNoTracking()
            .Include(u => u.Organization)
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Remove(User entity)
    {
        context.Users.Remove(entity);
    }

    public void Update(User entity)
    {
        context.Users.Update(entity);
    }
}

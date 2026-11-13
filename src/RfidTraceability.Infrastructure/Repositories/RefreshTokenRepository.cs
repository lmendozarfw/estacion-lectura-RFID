using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class RefreshTokenRepository(AppDbContext context) : IRefreshTokenRepository
{
    public async Task<RefreshToken> AddAsync(RefreshToken entity, CancellationToken cancellationToken = default)
    {
        var value = await context.RefreshTokens.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.RefreshTokens.AsQueryable().AsNoTracking().AnyAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<RefreshToken>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.RefreshTokens.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.RefreshTokens.AsQueryable().AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default)
    {
        return await context.RefreshTokens.AsQueryable()
            .Include(t => t.User)
                .ThenInclude(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(t => t.TokenHash == tokenHash, cancellationToken);
    }

    public void Remove(RefreshToken entity)
    {
        context.RefreshTokens.Remove(entity);
    }

    public void Update(RefreshToken entity)
    {
        context.RefreshTokens.Update(entity);
    }
}

using Microsoft.EntityFrameworkCore;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;
using RfidTraceability.Infrastructure.Persistence;

namespace RfidTraceability.Infrastructure.Repositories;

public class BlockRepository(AppDbContext context) : IBlockRepository
{
    public async Task<Block> AddAsync(Block entity, CancellationToken cancellationToken = default)
    {
        var value = await context.Blocks.AddAsync(entity, cancellationToken);
        return value.Entity;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var block = await context.Blocks.AsQueryable().AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        return block != null;
    }

    public async Task<IReadOnlyList<Block>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Blocks.AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Block?> GetByCodeAsync(Guid programId, string code, CancellationToken cancellationToken = default)
    {
        return await context.Blocks.AsQueryable().AsNoTracking().FirstOrDefaultAsync(b => b.ProgramId == programId && b.Code == code, cancellationToken);
    }

    public async Task<Block?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Blocks.AsQueryable().AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<Block?> GetWithLotsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Blocks.AsQueryable().AsNoTracking().Include(b => b.Lots).FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Block>> ListByProgramAsync(Guid programId, CancellationToken cancellationToken = default)
    {
        return await context.Blocks.AsQueryable().AsNoTracking().Where(b => b.ProgramId == programId).ToListAsync(cancellationToken);
    }

    public void Remove(Block entity)
    {
        context.Blocks.Remove(entity);
    }

    public void Update(Block entity)
    {
        context.Blocks.Update(entity);
    }
}
namespace RfidTraceability.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

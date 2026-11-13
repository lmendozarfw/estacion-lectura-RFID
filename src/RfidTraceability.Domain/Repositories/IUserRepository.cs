using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(Guid organizationId, string username, CancellationToken cancellationToken = default);

    Task<bool> ExistsByUsernameAsync(Guid organizationId, string username, CancellationToken cancellationToken = default);

    Task<User?> GetWithOrganizationAsync(Guid id, CancellationToken cancellationToken = default);
}

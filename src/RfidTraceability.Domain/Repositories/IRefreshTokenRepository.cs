using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Domain.Repositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default);
}

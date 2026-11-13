namespace RfidTraceability.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string TokenHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? ReplacedByTokenHash { get; set; }

    public User User { get; set; } = null!;

    public bool IsActive => RevokedAt is null && ExpiresAt > DateTime.UtcNow;

    public RefreshToken(Guid userId, string tokenHash, DateTime expiresAt)
    {
        UserId = DomainGuard.GuidNotEmpty(userId, nameof(userId));
        TokenHash = DomainGuard.NotNullOrWhiteSpace(tokenHash, nameof(tokenHash));
        ExpiresAt = expiresAt;
    }

    public void Revoke(string? replacedByTokenHash = null)
    {
        RevokedAt = DateTime.UtcNow;
        ReplacedByTokenHash = DomainGuard.NormalizeString(replacedByTokenHash, nameof(replacedByTokenHash));
    }
}

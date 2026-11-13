namespace RfidTraceability.Domain.Entities;

public class UserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;

    public UserRole(Guid userId, Guid roleId)
    {
        UserId = DomainGuard.GuidNotEmpty(userId, nameof(userId));
        RoleId = DomainGuard.GuidNotEmpty(roleId, nameof(roleId));
    }
}

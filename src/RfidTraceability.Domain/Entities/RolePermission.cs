namespace RfidTraceability.Domain.Entities;

public class RolePermission
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }

    public Role Role { get; set; } = null!;
    public Permission Permission { get; set; } = null!;

    public RolePermission(Guid roleId, Guid permissionId)
    {
        RoleId = DomainGuard.GuidNotEmpty(roleId, nameof(roleId));
        PermissionId = DomainGuard.GuidNotEmpty(permissionId, nameof(permissionId));
    }
}

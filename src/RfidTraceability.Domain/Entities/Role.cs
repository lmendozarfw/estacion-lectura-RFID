namespace RfidTraceability.Domain.Entities;

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Active { get; set; } = true;

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public Role(string code, string name, string? description)
    {
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        Name = DomainGuard.NotNullOrWhiteSpace(name, nameof(name));
        Description = DomainGuard.NormalizeString(description, nameof(description));
    }
}

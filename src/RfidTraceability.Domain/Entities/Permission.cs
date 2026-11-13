namespace RfidTraceability.Domain.Entities;

public class Permission
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ModuleId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Active { get; set; } = true;

    public Module Module { get; set; } = null!;
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public Permission(Guid moduleId, string code, string name, string? description)
    {
        ModuleId = DomainGuard.GuidNotEmpty(moduleId, nameof(moduleId));
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        Name = DomainGuard.NotNullOrWhiteSpace(name, nameof(name));
        Description = DomainGuard.NormalizeString(description, nameof(description));
    }
}

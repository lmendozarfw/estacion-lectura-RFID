namespace RfidTraceability.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrganizationId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Organization Organization { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public User(Guid organizationId, string username, string passwordHash, string employeeId)
    {
        OrganizationId = DomainGuard.GuidNotEmpty(organizationId, nameof(organizationId));
        Username = DomainGuard.NotNullOrWhiteSpace(username, nameof(username));
        PasswordHash = DomainGuard.NotNullOrWhiteSpace(passwordHash, nameof(passwordHash));
        EmployeeId = DomainGuard.NotNullOrWhiteSpace(employeeId, nameof(employeeId));
    }
}

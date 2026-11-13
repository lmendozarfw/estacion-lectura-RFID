namespace RfidTraceability.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrganizationId { get; set; }
    public Guid? EmployeeId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Organization Organization { get; set; } = null!;
    public Employee? Employee { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public User(Guid organizationId, string username, string passwordHash, Guid? employeeId = null)
    {
        OrganizationId = DomainGuard.GuidNotEmpty(organizationId, nameof(organizationId));
        Username = DomainGuard.NotNullOrWhiteSpace(username, nameof(username));
        PasswordHash = DomainGuard.NotNullOrWhiteSpace(passwordHash, nameof(passwordHash));
        EmployeeId = employeeId;
    }
}

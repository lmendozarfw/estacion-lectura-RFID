namespace RfidTraceability.Domain.Entities;

public class BusinessUnit
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public Guid OrganizationId { get; set; }

    public Organization Organization { get; set; } = null!;
    public ICollection<ControlPoint> ControlPoints { get; set; } = new List<ControlPoint>();

    public BusinessUnit(string code, string name, Guid organizationId)
    {
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        Name = DomainGuard.NotNullOrWhiteSpace(name, nameof(name));
        OrganizationId = DomainGuard.GuidNotEmpty(organizationId, nameof(organizationId));
    }
}

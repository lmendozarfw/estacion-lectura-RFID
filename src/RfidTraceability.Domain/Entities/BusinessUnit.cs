namespace RfidTraceability.Domain.Entities;

public class BusinessUnit
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public string OrganizationId { get; set; } = string.Empty;

    public Organization Organization { get; set; } = null!;
    public ICollection<ControlPoint> ControlPoints { get; set; } = new List<ControlPoint>();
}

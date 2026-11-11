namespace RfidTraceability.Domain.Entities;

public class ControlPoint
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Active { get; set; } = true;
    public Guid BusinessUnitId { get; set; }

    public BusinessUnit BusinessUnit { get; set; } = null!;
    public ICollection<RfidReader> Readers { get; set; } = new List<RfidReader>();
    public ICollection<PieceControlPointAdvanceReading> AdvanceReadings { get; set; } = new List<PieceControlPointAdvanceReading>();
    public ICollection<ProgramGoal> Goals { get; set; } = new List<ProgramGoal>();

    public ControlPoint(string code, string name, string? description, Guid businessUnitId)
    {
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        Name = DomainGuard.NotNullOrWhiteSpace(name, nameof(name));
        Description = DomainGuard.NormalizeString(description, nameof(description));
        BusinessUnitId = DomainGuard.GuidNotEmpty(businessUnitId, nameof(businessUnitId));
    }
}

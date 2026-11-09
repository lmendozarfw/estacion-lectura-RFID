namespace RfidTraceability.Domain.Entities;

public class ControlPoint
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Active { get; set; } = true;
    public string BusinessUnitId { get; set; } = string.Empty;

    public BusinessUnit BusinessUnit { get; set; } = null!;
    public ICollection<RfidReader> Readers { get; set; } = new List<RfidReader>();
    public ICollection<PieceControlPointAdvanceReading> AdvanceReadings { get; set; } = new List<PieceControlPointAdvanceReading>();
    public ICollection<ProgramGoal> Goals { get; set; } = new List<ProgramGoal>();
}

namespace RfidTraceability.Domain.Entities;

public class ControlPoint
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;

    public ICollection<PieceControlPointAdvanceReading> AdvanceReadings { get; set; } = new List<PieceControlPointAdvanceReading>();
    public ICollection<ProgramGoal> Goals { get; set; } = new List<ProgramGoal>();

    public ControlPoint(string name)
    {
        Name = DomainGuard.NotNullOrWhiteSpace(name, nameof(name));
    }
}

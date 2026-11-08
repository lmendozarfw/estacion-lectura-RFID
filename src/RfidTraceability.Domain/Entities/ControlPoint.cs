namespace RfidTraceability.Domain.Entities;

public class ControlPoint
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;

    public ICollection<PieceControlPointAdvanceReading> AdvanceReadings { get; set; } = new List<PieceControlPointAdvanceReading>();
    public ICollection<ProgramGoal> Goals { get; set; } = new List<ProgramGoal>();
}

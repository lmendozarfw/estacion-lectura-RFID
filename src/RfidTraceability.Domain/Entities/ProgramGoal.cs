namespace RfidTraceability.Domain.Entities;

public class ProgramGoal
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProgramId { get; set; } = string.Empty;
    public string ControlPointId { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public int TargetQuantity { get; set; }

    public Program Program { get; set; } = null!;
    public ControlPoint ControlPoint { get; set; } = null!;
}

namespace RfidTraceability.Domain.Entities;

public class ProgramGoal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProgramId { get; set; }
    public Guid ControlPointId { get; set; }
    public DateOnly Date { get; set; }
    public int TargetQuantity { get; set; }

    public Program Program { get; set; } = null!;
    public ControlPoint ControlPoint { get; set; } = null!;

    public ProgramGoal(Guid programId, Guid controlPointId, DateOnly date, int targetQuantity)
    {
        ProgramId = DomainGuard.GuidNotEmpty(programId, nameof(programId));
        ControlPointId = DomainGuard.GuidNotEmpty(controlPointId, nameof(controlPointId));
        Date = DomainGuard.NormalizedDateOnly(date, nameof(date));
        TargetQuantity = DomainGuard.GreaterThanZero(targetQuantity, nameof(targetQuantity));
    }
}

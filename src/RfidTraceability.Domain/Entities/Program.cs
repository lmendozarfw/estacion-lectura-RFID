using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Domain.Entities;

public class Program
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public int TargetQuantity { get; set; }
    public DateOnly? PlannedStartDate { get; set; }
    public DateOnly? PlannedEndDate { get; set; }
    public DateOnly? ActualStartDate { get; set; }
    public DateOnly? ActualEndDate { get; set; }
    public ProgramStatus Status { get; set; } = ProgramStatus.Planned;
    public Guid PurchaseOrderId { get; set; }

    public PurchaseOrder PurchaseOrder { get; set; } = null!;
    public ICollection<Block> Blocks { get; set; } = new List<Block>();
    public ICollection<ProgramGoal> Goals { get; set; } = new List<ProgramGoal>();

    public Program(string code, int targetQuantity, DateOnly? plannedStartDate, DateOnly? plannedEndDate, DateOnly? actualStartDate, DateOnly? actualEndDate, Guid purchaseOrderId)
    {
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        TargetQuantity = DomainGuard.GreaterThanZero(targetQuantity, nameof(targetQuantity));
        PlannedStartDate = DomainGuard.NormalizedDateOnly(plannedStartDate, nameof(plannedStartDate));
        PlannedEndDate = DomainGuard.NormalizedDateOnly(plannedEndDate, nameof(plannedEndDate));
        ActualStartDate = DomainGuard.NormalizedDateOnly(actualStartDate, nameof(actualStartDate));
        ActualEndDate = DomainGuard.NormalizedDateOnly(actualEndDate, nameof(actualEndDate));
        PurchaseOrderId = DomainGuard.GuidNotEmpty(purchaseOrderId, nameof(purchaseOrderId));
    }
}

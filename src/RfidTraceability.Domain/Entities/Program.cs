using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Domain.Entities;

public class Program
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public int TargetQuantity { get; set; }
    public DateOnly? PlannedStartDate { get; set; }
    public DateOnly? PlannedEndDate { get; set; }
    public DateOnly? ActualStartDate { get; set; }
    public DateOnly? ActualEndDate { get; set; }
    public ProgramStatus Status { get; set; } = ProgramStatus.Planned;
    public string PurchaseOrderId { get; set; } = string.Empty;

    public PurchaseOrder PurchaseOrder { get; set; } = null!;
    public ICollection<Block> Blocks { get; set; } = new List<Block>();
    public ICollection<ProgramGoal> Goals { get; set; } = new List<ProgramGoal>();
}

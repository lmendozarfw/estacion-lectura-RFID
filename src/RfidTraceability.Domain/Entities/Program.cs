namespace RfidTraceability.Domain.Entities;

public class Program
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public string PurchaseOrderId { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public PurchaseOrder PurchaseOrder { get; set; } = null!;
    public ICollection<Lot> Lots { get; set; } = new List<Lot>();
    public ICollection<ProgramGoal> Goals { get; set; } = new List<ProgramGoal>();
}

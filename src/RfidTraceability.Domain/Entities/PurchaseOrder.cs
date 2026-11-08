namespace RfidTraceability.Domain.Entities;

public class PurchaseOrder
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public DateTime? Date { get; set; }

    public ICollection<Program> Programs { get; set; } = new List<Program>();
}

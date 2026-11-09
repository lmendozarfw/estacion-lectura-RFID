using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Domain.Entities;

public class PurchaseOrder
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public DateOnly? Date { get; set; }
    public string OrganizationId { get; set; } = string.Empty;
    public DataOrigin Origin { get; set; } = DataOrigin.Local;
    public string? ExternalId { get; set; }

    public Organization Organization { get; set; } = null!;
    public ICollection<Program> Programs { get; set; } = new List<Program>();
}

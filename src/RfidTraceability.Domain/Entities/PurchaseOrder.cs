using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Domain.Entities;

public class PurchaseOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public DateOnly? Date { get; set; }
    public Guid OrganizationId { get; set; }
    public DataOrigin Origin { get; set; } = DataOrigin.Local;
    public string? ExternalId { get; set; }

    public Organization Organization { get; set; } = null!;
    public ICollection<Program> Programs { get; set; } = new List<Program>();

    public PurchaseOrder(string code, DateOnly? date, Guid organizationId, DataOrigin origin, string? externalId)
    {
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        Date = DomainGuard.NormalizedDateOnly(date, nameof(date));
        OrganizationId = DomainGuard.GuidNotEmpty(organizationId, nameof(organizationId));
        Origin = DomainGuard.IsInEnum<DataOrigin>(origin, nameof(origin));
        ExternalId = DomainGuard.NormalizeString(externalId, nameof(externalId));
    }
}

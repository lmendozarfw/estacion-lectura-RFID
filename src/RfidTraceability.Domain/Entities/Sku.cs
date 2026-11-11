namespace RfidTraceability.Domain.Entities;

public class Sku
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Active { get; set; } = true;
    public Guid OrganizationId { get; set; }

    public Organization Organization { get; set; } = null!;
    public ICollection<RfidPiece> RfidPieces { get; set; } = new List<RfidPiece>();

    public Sku(string code, string? description, Guid organizationId)
    {
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        Description = DomainGuard.NormalizeString(description, nameof(description));
        OrganizationId = DomainGuard.GuidNotEmpty(organizationId, nameof(organizationId));
    }
}

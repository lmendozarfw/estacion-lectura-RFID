namespace RfidTraceability.Domain.Entities;

public class Sku
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Active { get; set; } = true;
    public string OrganizationId { get; set; } = string.Empty;

    public Organization Organization { get; set; } = null!;
    public ICollection<RfidPiece> RfidPieces { get; set; } = new List<RfidPiece>();
}

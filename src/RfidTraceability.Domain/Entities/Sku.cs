namespace RfidTraceability.Domain.Entities;

public class Sku
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;

    public ICollection<RfidPiece> RfidPieces { get; set; } = new List<RfidPiece>();
}

namespace RfidTraceability.Domain.Entities;

public class RfidPiece
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Epc { get; set; } = string.Empty;
    public string SkuId { get; set; } = string.Empty;
    public string LotId { get; set; } = string.Empty;
    public string SideCode { get; set; } = string.Empty;

    public Sku Sku { get; set; } = null!;
    public Lot Lot { get; set; } = null!;
    public ICollection<PieceControlPointAdvanceReading> AdvanceReadings { get; set; } = new List<PieceControlPointAdvanceReading>();
}

namespace RfidTraceability.Domain.Entities;

public class RfidPiece
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Epc { get; set; } = string.Empty;
    public Guid SkuId { get; set; }
    public Guid LotId { get; set; }
    public string SideCode { get; set; } = string.Empty;

    public Sku Sku { get; set; } = null!;
    public Lot Lot { get; set; } = null!;
    public ICollection<PieceControlPointAdvanceReading> AdvanceReadings { get; set; } = new List<PieceControlPointAdvanceReading>();

    public RfidPiece(string epc, Guid skuId, Guid lotId, string sideCode)
    {
        Epc = DomainGuard.NotNullOrWhiteSpace(epc, nameof(epc));
        SkuId = DomainGuard.GuidNotEmpty(skuId, nameof(skuId));
        LotId = DomainGuard.GuidNotEmpty(lotId, nameof(lotId));
        SideCode = DomainGuard.NotNullOrWhiteSpace(sideCode, nameof(sideCode));    
    }
}

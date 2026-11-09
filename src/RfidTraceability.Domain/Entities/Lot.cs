namespace RfidTraceability.Domain.Entities;

public class Lot
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public int ExpectedPairs { get; set; }
    public string BlockId { get; set; } = string.Empty;

    public Block Block { get; set; } = null!;
    public ICollection<RfidPiece> RfidPieces { get; set; } = new List<RfidPiece>();
}

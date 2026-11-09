namespace RfidTraceability.Domain.Entities;

public class Lot
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public int ExpectedPairs { get; set; }
    public Guid BlockId { get; set; }

    public Block Block { get; set; } = null!;
    public ICollection<RfidPiece> RfidPieces { get; set; } = new List<RfidPiece>();

    public Lot(string code, int expectedPairs, Guid blockId)
    {
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        ExpectedPairs = DomainGuard.GreaterThanZero(expectedPairs, nameof(expectedPairs));
        BlockId = DomainGuard.GuidNotEmpty(blockId, nameof(blockId));
    }
}

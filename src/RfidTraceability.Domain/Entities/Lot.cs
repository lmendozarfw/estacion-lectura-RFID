namespace RfidTraceability.Domain.Entities;

public class Lot
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public string ProgramId { get; set; } = string.Empty;
    public int ExpectedPieces { get; set; }

    public Program Program { get; set; } = null!;
    public ICollection<RfidPiece> RfidPieces { get; set; } = new List<RfidPiece>();
}

namespace RfidTraceability.Domain.Entities;

public class Lot
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public Guid ProgramId { get; set; }
    public int ExpectedPieces { get; set; }

    public Program Program { get; set; } = null!;
    public ICollection<RfidPiece> RfidPieces { get; set; } = new List<RfidPiece>();

    public Lot(string code, Guid programId, int expectedPieces = 10)
    {
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        ProgramId = DomainGuard.GuidNotEmpty(programId, nameof(programId));
        ExpectedPieces = DomainGuard.GreaterThanZero(expectedPieces, nameof(expectedPieces));
    }
}

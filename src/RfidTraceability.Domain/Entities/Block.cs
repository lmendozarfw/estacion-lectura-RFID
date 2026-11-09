namespace RfidTraceability.Domain.Entities;

public class Block
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public Guid ProgramId { get; set; }

    public Program Program { get; set; } = null!;
    public ICollection<Lot> Lots { get; set; } = new List<Lot>();

    public Block(string code, Guid programId)
    {
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        ProgramId = DomainGuard.GuidNotEmpty(programId, nameof(programId));
    }
}

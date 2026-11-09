namespace RfidTraceability.Domain.Entities;

public class Block
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public string ProgramId { get; set; } = string.Empty;

    public Program Program { get; set; } = null!;
    public ICollection<Lot> Lots { get; set; } = new List<Lot>();
}

namespace RfidTraceability.Domain.Entities;

public class PieceControlPointAdvanceReading
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string SourceEventId { get; set; } = string.Empty;
    public string RfidPieceId { get; set; } = string.Empty;
    public string ControlPointId { get; set; } = string.Empty;
    public string RfidReaderId { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public RfidPiece RfidPiece { get; set; } = null!;
    public ControlPoint ControlPoint { get; set; } = null!;
    public RfidReader RfidReader { get; set; } = null!;
}

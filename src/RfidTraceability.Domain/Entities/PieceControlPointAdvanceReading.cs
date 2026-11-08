namespace RfidTraceability.Domain.Entities;

public class PieceControlPointAdvanceReading
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string RfidPieceId { get; set; } = string.Empty;
    public string ControlPointId { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public RfidPiece RfidPiece { get; set; } = null!;
    public ControlPoint ControlPoint { get; set; } = null!;
}

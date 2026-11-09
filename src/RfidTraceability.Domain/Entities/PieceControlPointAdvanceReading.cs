namespace RfidTraceability.Domain.Entities;

public class PieceControlPointAdvanceReading
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid RfidPieceId { get; private set; }
    public Guid ControlPointId { get; private set; }
    public DateTime Date { get; private set; }

    public RfidPiece RfidPiece { get; set; } = null!;
    public ControlPoint ControlPoint { get; set; } = null!;

    public PieceControlPointAdvanceReading(Guid rfidPieceId, Guid controlPointId)
    {
        RfidPieceId = DomainGuard.GuidNotEmpty(rfidPieceId, nameof(rfidPieceId));
        ControlPointId = DomainGuard.GuidNotEmpty(controlPointId, nameof(controlPointId));
        Date = DateTime.UtcNow;
    }
}

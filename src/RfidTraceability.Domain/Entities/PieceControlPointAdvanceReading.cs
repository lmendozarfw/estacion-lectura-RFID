namespace RfidTraceability.Domain.Entities;

public class PieceControlPointAdvanceReading
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SourceEventId { get; set; }
    public Guid RfidPieceId { get; set; }
    public Guid ControlPointId { get; set; }
    public Guid RfidReaderId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public RfidPiece RfidPiece { get; set; } = null!;
    public ControlPoint ControlPoint { get; set; } = null!;
    public RfidReader RfidReader { get; set; } = null!;

    public PieceControlPointAdvanceReading(Guid sourceEventId, Guid rfidPieceId, Guid controlPointId, Guid rfidReaderId)
    {
        SourceEventId = DomainGuard.GuidNotEmpty(sourceEventId, nameof(sourceEventId));
        RfidPieceId = DomainGuard.GuidNotEmpty(rfidPieceId, nameof(rfidPieceId));
        ControlPointId = DomainGuard.GuidNotEmpty(controlPointId, nameof(controlPointId));
        RfidReaderId = DomainGuard.GuidNotEmpty(rfidReaderId, nameof(rfidReaderId));
    }
}

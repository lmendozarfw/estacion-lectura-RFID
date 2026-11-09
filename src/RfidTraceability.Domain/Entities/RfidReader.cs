using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Domain.Entities;

public class RfidReader
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public ReaderStatus Status { get; set; } = ReaderStatus.Active;
    public string ControlPointId { get; set; } = string.Empty;

    public ControlPoint ControlPoint { get; set; } = null!;
    public ICollection<PieceControlPointAdvanceReading> AdvanceReadings { get; set; } = new List<PieceControlPointAdvanceReading>();
}

using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Domain.Entities;

public class RfidReader
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public ReaderStatus Status { get; set; } = ReaderStatus.Active;
    public Guid ControlPointId { get; set; }

    public ControlPoint ControlPoint { get; set; } = null!;
    public ICollection<PieceControlPointAdvanceReading> AdvanceReadings { get; set; } = new List<PieceControlPointAdvanceReading>();

    public RfidReader(string code, string serialNumber, ReaderStatus status, Guid controlPointId)
    {
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        SerialNumber = DomainGuard.NotNullOrWhiteSpace(serialNumber, nameof(serialNumber));
        Status = DomainGuard.IsInEnum<ReaderStatus>(status, nameof(status));
        ControlPointId = DomainGuard.GuidNotEmpty(controlPointId, nameof(controlPointId));
    }
}

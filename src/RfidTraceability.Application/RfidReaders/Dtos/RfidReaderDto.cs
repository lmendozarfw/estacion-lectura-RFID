using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Application.RfidReaders.Dtos;

public record RfidReaderDto(
    Guid Id,
    string Code,
    string SerialNumber,
    ReaderStatus Status,
    Guid ControlPointId
);

public static class RfidReaderMappingExtensions
{
    public static RfidReaderDto ToMap(this RfidReader entity) => new RfidReaderDto(
            entity.Id,
            entity.Code,
            entity.SerialNumber,
            entity.Status,
            entity.ControlPointId
        );
}

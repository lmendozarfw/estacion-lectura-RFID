using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Application.ControlPoints.Dtos;

public record ControlPointDto(
    Guid Id,
    string Code,
    string Name,
    string? Description,
    bool Active,
    Guid BusinessUnitId
);

public static class ControlPointMappingExtensions
{
    public static ControlPointDto ToMap(this ControlPoint entity) => new ControlPointDto(
            entity.Id,
            entity.Code,
            entity.Name,
            entity.Description,
            entity.Active,
            entity.BusinessUnitId
        );
}

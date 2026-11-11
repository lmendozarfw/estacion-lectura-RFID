using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Application.BusinessUnits.Dtos;

public record BusinessUnitDto(
    Guid Id,
    string Code,
    string Name,
    Guid OrganizationId
);

public static class BusinessUnitMappingExtensions
{
    public static BusinessUnitDto ToMap(this BusinessUnit entity) => new BusinessUnitDto(
            entity.Id,
            entity.Code,
            entity.Name,
            entity.OrganizationId
        );
}
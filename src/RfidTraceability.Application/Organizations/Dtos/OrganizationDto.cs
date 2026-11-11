using RfidTraceability.Domain.Entities;

public record OrganizationDto(
    Guid Id,
    string Code,
    string Name
);

public static class OrganizationsMappingExtensions {
    public static OrganizationDto ToMap(this Organization entity) => new OrganizationDto(entity.Id, entity.Code, entity.Name);
}
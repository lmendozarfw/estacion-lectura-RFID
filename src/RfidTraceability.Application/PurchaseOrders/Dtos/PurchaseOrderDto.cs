using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Application.PurchaseOrders.Dtos;

public record PurchaseOrderDto(
    Guid Id,
    string Code,
    DateOnly? Date,
    Guid OrganizationId,
    DataOrigin Origin,
    string? ExternalId
);

public static class PurchaseOrderMappingExtensions
{
    public static PurchaseOrderDto ToMap(this PurchaseOrder entity) => new PurchaseOrderDto(
            entity.Id,
            entity.Code,
            entity.Date,
            entity.OrganizationId,
            entity.Origin,
            entity.ExternalId
        );
}

using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Application.Programs.Dtos;

public record ProgramDto(
    Guid Id,
    string Code,
    int TargetQuantity,
    DateOnly? PlannedStartDate,
    DateOnly? PlannedEndDate,
    DateOnly? ActualStartDate,
    DateOnly? ActualEndDate,
    ProgramStatus Status,
    Guid PurchaseOrderId
);

public static class ProgramMappingExtensions
{
    public static ProgramDto ToMap(this Program entity) => new ProgramDto(
            entity.Id,
            entity.Code,
            entity.TargetQuantity,
            entity.PlannedStartDate,
            entity.PlannedEndDate,
            entity.ActualStartDate,
            entity.ActualEndDate,
            entity.Status,
            entity.PurchaseOrderId
        );
}

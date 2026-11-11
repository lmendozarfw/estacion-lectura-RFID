using RfidTraceability.Domain.Entities;

namespace RfidTraceability.Application.ProgramGoals.Dtos;

public record ProgramGoalDto(
    Guid Id,
    Guid ProgramId,
    Guid ControlPointId,
    DateOnly Date,
    int TargetQuantity
);

public static class ProgramGoalMappingExtensions
{
    public static ProgramGoalDto ToMap(this ProgramGoal entity) => new ProgramGoalDto(
            entity.Id,
            entity.ProgramId,
            entity.ControlPointId,
            entity.Date,
            entity.TargetQuantity
        );
}

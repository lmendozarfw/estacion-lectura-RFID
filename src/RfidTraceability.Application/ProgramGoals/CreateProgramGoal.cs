using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

public record CreateProgramGoalCommand(
    Guid ProgramId,
    Guid ControlPointId,
    DateOnly Date,
    int TargetQuantity
) : IRequest<Guid>;

public class CreateProgramGoalHandler
(
    IProgramGoalRepository programGoalRepository,
    IControlPointRepository controlPointRepository,
    IProgramRepository programRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateProgramGoalCommand, Guid>
{
    public async Task<Guid> Handle(CreateProgramGoalCommand request, CancellationToken cancellationToken)
    {
        if (!await programRepository.ExistsAsync(request.ProgramId))
        {
            throw new EntityNotFoundException(nameof(Program), request.ProgramId);
        }
        if (!await controlPointRepository.ExistsAsync(request.ControlPointId))
        {
            throw new EntityNotFoundException(nameof(ControlPoint), request.ControlPointId);
        }
        if (request.Date < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new InvalidDateException($"La fecha meta del programa no puede ser anterior al día actual.");
        }
        if (request.TargetQuantity <= 0)
        {
            throw new DomainException($"La cantidad meta de pares debe ser mayor a cero.");
        }
        var programGoal = new ProgramGoal(
            request.ProgramId, request.ControlPointId, request.Date, request.TargetQuantity
        );
        await programGoalRepository.AddAsync(programGoal, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return programGoal.Id;
    }
}
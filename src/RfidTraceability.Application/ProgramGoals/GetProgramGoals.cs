using MediatR;
using RfidTraceability.Application.ProgramGoals.Dtos;
using RfidTraceability.Domain.Repositories;

public record GetProgramGoalsQuery() : IRequest<List<ProgramGoalDto>>;

public class GetProgramGoalsHandler
(
    IProgramGoalRepository programGoalRepository
) : IRequestHandler<GetProgramGoalsQuery, List<ProgramGoalDto>>
{
    public async Task<List<ProgramGoalDto>> Handle(GetProgramGoalsQuery request, CancellationToken cancellationToken)
    {
        var programGoals = await programGoalRepository.GetAllAsync(cancellationToken);
        return programGoals.Select(g => g.ToMap()).ToList();
    }
}

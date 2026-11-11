using MediatR;
using RfidTraceability.Application.Programs.Dtos;
using RfidTraceability.Domain.Repositories;

public record GetProgramsQuery() : IRequest<List<ProgramDto>>;

public class GetProgramsHandler
(
    IProgramRepository programRepository
) : IRequestHandler<GetProgramsQuery, List<ProgramDto>>
{
    public async Task<List<ProgramDto>> Handle(GetProgramsQuery request, CancellationToken cancellationToken)
    {
        var programs = await programRepository.GetAllAsync(cancellationToken);
        return programs.Select(p => p.ToMap()).ToList();
    }
}

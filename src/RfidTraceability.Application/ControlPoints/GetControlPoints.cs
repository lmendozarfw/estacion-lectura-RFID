using MediatR;
using RfidTraceability.Application.ControlPoints.Dtos;
using RfidTraceability.Domain.Repositories;

public record GetControlPointsQuery() : IRequest<List<ControlPointDto>>;

public class GetControlPointsHandler
(
    IControlPointRepository controlPointRepository
) : IRequestHandler<GetControlPointsQuery, List<ControlPointDto>>
{
    public async Task<List<ControlPointDto>> Handle(GetControlPointsQuery request, CancellationToken cancellationToken)
    {
        var controlPoints = await controlPointRepository.GetAllAsync(cancellationToken);
        return controlPoints.Select(c => c.ToMap()).ToList();
    }
}

using MediatR;
using RfidTraceability.Application.BusinessUnits.Dtos;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

public record GetBusinessUnitsQuery() : IRequest<List<BusinessUnitDto>>;

public class GetBusinessUnitsHandler
(
    IBusinessUnitRepository businessUnitRepository
) : IRequestHandler<GetBusinessUnitsQuery, List<BusinessUnitDto>>
{
    public async Task<List<BusinessUnitDto>> Handle(GetBusinessUnitsQuery request, CancellationToken cancellationToken)
    {
        var businessUnits = await businessUnitRepository.GetAllAsync(cancellationToken);
        return businessUnits.Select(b => b.ToMap()).ToList();
    }
}
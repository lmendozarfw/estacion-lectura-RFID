using MediatR;
using RfidTraceability.Domain.Repositories;

public record GetOrganizationsQuery() : IRequest<List<OrganizationDto>>;

public class GetOrganizationsHandler(IOrganizationRepository organizationRepository) : IRequestHandler<GetOrganizationsQuery, List<OrganizationDto>>
{
    public async Task<List<OrganizationDto>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var organizations = await organizationRepository.GetAllAsync(cancellationToken);
        return organizations.Select(o => o.ToMap()).ToList();
    }
}
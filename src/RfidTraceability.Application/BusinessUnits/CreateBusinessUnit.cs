using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.BusinessUnits;

public sealed record CreateBusinessUnitCommand(Guid OrganizationId, string Code, string Name) : IRequest<Guid>;

public sealed class CreateBusinessUnitHandler(
    IBusinessUnitRepository businessUnits,
    IOrganizationRepository organizations,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateBusinessUnitCommand, Guid>
{
    public async Task<Guid> Handle(CreateBusinessUnitCommand request, CancellationToken cancellationToken)
    {
        if (!await organizations.ExistsAsync(request.OrganizationId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(Organization), request.OrganizationId);
        }

        if (await businessUnits.GetByCodeAsync(request.OrganizationId, request.Code, cancellationToken) is not null)
        {
            throw new DuplicateException($"Ya existe una unidad de negocio con el código '{request.Code}' para la organización.");
        }

        var businessUnit = new BusinessUnit(request.Code, request.Name, request.OrganizationId);
        await businessUnits.AddAsync(businessUnit, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return businessUnit.Id;
    }
}

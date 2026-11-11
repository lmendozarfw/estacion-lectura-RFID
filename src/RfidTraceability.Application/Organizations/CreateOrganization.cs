using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.Organizations;

public sealed record CreateOrganizationCommand(string Code, string Name) : IRequest<Guid>;

public sealed class CreateOrganizationHandler(
    IOrganizationRepository organizations,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateOrganizationCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        if (await organizations.ExistsByCodeAsync(request.Code, cancellationToken))
        {
            throw new DuplicateException($"Ya existe una organización con el código '{request.Code}'.");
        }

        var organization = new Organization(request.Code, request.Name);
        await organizations.AddAsync(organization, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return organization.Id;
    }
}

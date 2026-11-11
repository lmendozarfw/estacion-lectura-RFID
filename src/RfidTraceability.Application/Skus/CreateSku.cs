using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.Skus;

public sealed record CreateSkuCommand(Guid OrganizationId, string Code, string? Description) : IRequest<Guid>;

public sealed class CreateSkuHandler(
    ISkuRepository skus,
    IOrganizationRepository organizations,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateSkuCommand, Guid>
{
    public async Task<Guid> Handle(CreateSkuCommand request, CancellationToken cancellationToken)
    {
        if (!await organizations.ExistsAsync(request.OrganizationId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(Organization), request.OrganizationId);
        }

        if (await skus.ExistsByCodeAsync(request.OrganizationId, request.Code, cancellationToken))
        {
            throw new DuplicateException($"Ya existe un SKU con el código '{request.Code}' para la organización.");
        }

        var sku = new Sku(request.Code, request.Description, request.OrganizationId);
        await skus.AddAsync(sku, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return sku.Id;
    }
}

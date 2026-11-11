using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Enums;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.PurchaseOrders;

public sealed record CreatePurchaseOrderCommand(Guid OrganizationId, string Code, DateOnly? Date, string? ExternalId) : IRequest<Guid>;

public sealed class CreatePurchaseOrderHandler(
    IPurchaseOrderRepository purchaseOrders,
    IOrganizationRepository organizations,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePurchaseOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        if (!await organizations.ExistsAsync(request.OrganizationId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(Organization), request.OrganizationId);
        }

        if (await purchaseOrders.ExistsByCodeAsync(request.OrganizationId, request.Code, cancellationToken))
        {
            throw new DuplicateException($"Ya existe una orden de compra con el código '{request.Code}' para la organización.");
        }

        var purchaseOrder = new PurchaseOrder(request.Code, request.Date, request.OrganizationId, DataOrigin.Local, request.ExternalId);
        await purchaseOrders.AddAsync(purchaseOrder, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return purchaseOrder.Id;
    }
}

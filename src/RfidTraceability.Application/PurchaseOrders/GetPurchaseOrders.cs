using MediatR;
using RfidTraceability.Application.PurchaseOrders.Dtos;
using RfidTraceability.Domain.Repositories;

public record GetPurchaseOrdersQuery() : IRequest<List<PurchaseOrderDto>>;

public class GetPurchaseOrdersHandler
(
    IPurchaseOrderRepository purchaseOrderRepository
) : IRequestHandler<GetPurchaseOrdersQuery, List<PurchaseOrderDto>>
{
    public async Task<List<PurchaseOrderDto>> Handle(GetPurchaseOrdersQuery request, CancellationToken cancellationToken)
    {
        var purchaseOrders = await purchaseOrderRepository.GetAllAsync(cancellationToken);
        return purchaseOrders.Select(p => p.ToMap()).ToList();
    }
}

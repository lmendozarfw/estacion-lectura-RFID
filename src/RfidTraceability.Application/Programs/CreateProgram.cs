using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.Programs;

public sealed record CreateProgramCommand(
    Guid PurchaseOrderId,
    string Code,
    int TargetQuantity,
    DateOnly? PlannedStartDate,
    DateOnly? PlannedEndDate) : IRequest<Guid>;

public sealed class CreateProgramHandler(
    IProgramRepository programs,
    IPurchaseOrderRepository purchaseOrders,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProgramCommand, Guid>
{
    public async Task<Guid> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
    {
        if (!await purchaseOrders.ExistsAsync(request.PurchaseOrderId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(PurchaseOrder), request.PurchaseOrderId);
        }

        if (await programs.GetByCodeAsync(request.PurchaseOrderId, request.Code, cancellationToken) is not null)
        {
            throw new DuplicateException($"Ya existe un programa con el código '{request.Code}' para la orden de compra.");
        }

        var program = new Program(request.Code, request.TargetQuantity, request.PlannedStartDate, request.PlannedEndDate, null, null, request.PurchaseOrderId);
        await programs.AddAsync(program, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return program.Id;
    }
}

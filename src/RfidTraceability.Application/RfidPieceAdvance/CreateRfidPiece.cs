using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Enums;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.RfidPieceAdvance;

public record CreateRfidPieceCommand(
    string Epc,
    Guid OrganizationId
) : IRequest<Guid>;

public class CreateRfidPieceHandler
(
    IOrganizationRepository organizationRepository,
    IRfidPieceRepository rfidPieceRepository,
    ISkuRepository skuRepository,
    IPurchaseOrderRepository purchaseOrderRepository,
    IProgramRepository programRepository,
    IBlockRepository blockRepository,
    ILotRepository lotRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateRfidPieceCommand, Guid>
{
    private const int EpcLength = 32;

    public async Task<Guid> Handle(CreateRfidPieceCommand request, CancellationToken cancellationToken)
    {
        if (!await organizationRepository.ExistsAsync(request.OrganizationId))
        {
            throw new EntityNotFoundException("No se ha encontrado la organización del usuario", nameof(request.OrganizationId));
        }
        if (await rfidPieceRepository.ExistsByEpcAsync(request.Epc))
        {
            throw new DuplicateException($"Ya existe una pieza con el mismo EPC {request.Epc}");
        }
        if (request.Epc.Length < EpcLength)
        {
            throw new DomainException($"El EPC '{request.Epc}' debe tener al menos {EpcLength} caracteres.");
        }

        //1234567891234-1234-12345-12-12-1234-12
        var sku = request.Epc.Substring(0, 13);
        var purchaseOrder = request.Epc.Substring(13, 4);
        var program = request.Epc.Substring(17, 5);
        var block = request.Epc.Substring(22, 2);
        var lot = request.Epc.Substring(26, 4);
        var side = request.Epc.Substring(30, 2);

        Guid skuId;
        if (!await skuRepository.ExistsByCodeAsync(request.OrganizationId, sku, cancellationToken))
        {
            skuId = (await skuRepository.AddAsync(new Sku(sku, sku, request.OrganizationId), cancellationToken)).Id;
        }
        else
        {
            skuId = (await skuRepository.GetByCodeAsync(request.OrganizationId, sku, cancellationToken))!.Id;
        }

        Guid purchaseOrderId;
        if (!await purchaseOrderRepository.ExistsByCodeAsync(request.OrganizationId, purchaseOrder, cancellationToken))
        {
            purchaseOrderId = (await purchaseOrderRepository.AddAsync(new PurchaseOrder(purchaseOrder, DateOnly.FromDateTime(DateTime.UtcNow), request.OrganizationId, DataOrigin.Local, ""), cancellationToken)).Id;
        }
        else
        {
            purchaseOrderId = (await purchaseOrderRepository.GetByCodeAsync(request.OrganizationId, purchaseOrder, cancellationToken))!.Id;
        }

        Guid programId;
        var existingProgram = await programRepository.GetByCodeAsync(purchaseOrderId, program, cancellationToken);
        if (existingProgram is null)
        {
            programId = (await programRepository.AddAsync(new Program(program, 1, null, null, null, null, purchaseOrderId), cancellationToken)).Id;
        }
        else
        {
            programId = existingProgram.Id;
        }

        Guid blockId;
        if (!await blockRepository.ExistsByCodeAsync(programId, block, cancellationToken))
        {
            blockId = (await blockRepository.AddAsync(new Block(block, programId))).Id;
        }
        else
        {
            blockId = (await blockRepository.GetByCodeAsync(programId, block, cancellationToken))!.Id;
        }

        Guid lotId;
        if (!await lotRepository.ExistsByCodeAsync(blockId, lot, cancellationToken))
        {
            lotId = (await lotRepository.AddAsync(new Lot(lot, blockId))).Id;
        }
        else
        {
            lotId = (await lotRepository.GetByCodeAsync(blockId, lot, cancellationToken))!.Id;
        }

        var piece = new RfidPiece(
            request.Epc,
            skuId,
            lotId,
            side
        );
        var rfidPiece = await rfidPieceRepository.AddAsync(piece, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return rfidPiece.Id;
    }
}

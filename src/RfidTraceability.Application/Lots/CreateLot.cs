using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.Lots;

public sealed record CreateLotCommand(Guid BlockId, string Code, int ExpectedPairs) : IRequest<Guid>;

public sealed class CreateLotHandler(
    ILotRepository lots,
    IBlockRepository blocks,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateLotCommand, Guid>
{
    public async Task<Guid> Handle(CreateLotCommand request, CancellationToken cancellationToken)
    {
        if (!await blocks.ExistsAsync(request.BlockId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(Block), request.BlockId);
        }

        if (await lots.GetByCodeAsync(request.BlockId, request.Code, cancellationToken) is not null)
        {
            throw new DuplicateException($"Ya existe un lote con el código '{request.Code}' para el bloque.");
        }

        var lot = new Lot(request.Code, request.BlockId, request.ExpectedPairs);
        await lots.AddAsync(lot, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return lot.Id;
    }
}

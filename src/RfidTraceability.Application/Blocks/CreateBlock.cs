using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.Blocks;

public sealed record CreateBlockCommand(Guid ProgramId, string Code) : IRequest<Guid>;

public sealed class CreateBlockHandler(
    IBlockRepository blocks,
    IProgramRepository programs,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateBlockCommand, Guid>
{
    public async Task<Guid> Handle(CreateBlockCommand request, CancellationToken cancellationToken)
    {
        if (!await programs.ExistsAsync(request.ProgramId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(Program), request.ProgramId);
        }

        if (await blocks.GetByCodeAsync(request.ProgramId, request.Code, cancellationToken) is not null)
        {
            throw new DuplicateException($"Ya existe un bloque con el código '{request.Code}' para el programa.");
        }

        var block = new Block(request.Code, request.ProgramId);
        await blocks.AddAsync(block, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return block.Id;
    }
}

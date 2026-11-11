using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Enums;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.RfidReaders;

public sealed record CreateRfidReaderCommand(Guid ControlPointId, string Code, string SerialNumber) : IRequest<Guid>;

public sealed class CreateRfidReaderHandler(
    IRfidReaderRepository rfidReaders,
    IControlPointRepository controlPoints,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateRfidReaderCommand, Guid>
{
    public async Task<Guid> Handle(CreateRfidReaderCommand request, CancellationToken cancellationToken)
    {
        if (!await controlPoints.ExistsAsync(request.ControlPointId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(ControlPoint), request.ControlPointId);
        }

        if (await rfidReaders.GetByCodeAsync(request.ControlPointId, request.Code, cancellationToken) is not null)
        {
            throw new DuplicateException($"Ya existe un lector RFID con el código '{request.Code}' para el punto de control.");
        }

        if (await rfidReaders.GetBySerialNumberAsync(request.SerialNumber, cancellationToken) is not null)
        {
            throw new DuplicateException($"Ya existe un lector RFID con el número de serie '{request.SerialNumber}'.");
        }

        var reader = new RfidReader(request.Code, request.SerialNumber, ReaderStatus.Active, request.ControlPointId);
        await rfidReaders.AddAsync(reader, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return reader.Id;
    }
}

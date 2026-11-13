using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Application.RfidPieceAdvance;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.Readings;

public sealed record RfidReadingEventInput(
    Guid EventId,
    string Epc,
    DateTime ReaderDate,
    Guid ReaderId);

public sealed record RegisterRfidReadingEventsCommand(
    IReadOnlyList<RfidReadingEventInput> Events,
    Guid OrganizationId) : IRequest<int>;

public sealed class RegisterRfidReadingEventsHandler(
    ISender sender,
    IRfidReaderRepository readers,
    IRfidPieceRepository pieces,
    IPieceControlPointAdvanceReadingRepository advanceReadings,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterRfidReadingEventsCommand, int>
{
    public async Task<int> Handle(RegisterRfidReadingEventsCommand request, CancellationToken cancellationToken)
    {
        var processed = 0;

        foreach (var readingEvent in request.Events)
        {
            if (await advanceReadings.ExistsBySourceEventIdAsync(readingEvent.EventId, cancellationToken))
            {
                continue;
            }

            var reader = await readers.GetByIdAsync(readingEvent.ReaderId, cancellationToken)
                ?? throw new EntityNotFoundException(nameof(RfidReader), readingEvent.ReaderId);

            var piece = await pieces.GetByEpcAsync(readingEvent.Epc, cancellationToken);

            var pieceId = piece is null
                ? await sender.Send(new CreateRfidPieceCommand(readingEvent.Epc, request.OrganizationId), cancellationToken)
                : piece.Id;

            var advanceReading = new PieceControlPointAdvanceReading(
                readingEvent.EventId,
                pieceId,
                reader.ControlPointId,
                reader.Id)
            {
                Date = readingEvent.ReaderDate
            };

            await advanceReadings.AddAsync(advanceReading, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            processed++;
        }

        return processed;
    }
}

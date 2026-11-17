using MediatR;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Application.RfidReaders.Dtos;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.RfidReaders;

public sealed record GetRfidReaderByIdQuery(Guid Id, Guid OrganizationId) : IRequest<RfidReaderConfigDto>;

public sealed class GetRfidReaderByIdHandler(
    IRfidReaderRepository readers,
    IControlPointRepository controlPoints,
    IBusinessUnitRepository businessUnits
) : IRequestHandler<GetRfidReaderByIdQuery, RfidReaderConfigDto>
{
    public async Task<RfidReaderConfigDto> Handle(GetRfidReaderByIdQuery request, CancellationToken cancellationToken)
    {
        var reader = await readers.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(RfidReader), request.Id);

        var controlPoint = await controlPoints.GetByIdAsync(reader.ControlPointId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(ControlPoint), reader.ControlPointId);

        var businessUnit = await businessUnits.GetByIdAsync(controlPoint.BusinessUnitId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(BusinessUnit), controlPoint.BusinessUnitId);

        if (businessUnit.OrganizationId != request.OrganizationId)
        {
            throw new UnauthorizedException("El lector no pertenece a la organización del usuario.");
        }

        return new RfidReaderConfigDto(
            reader.Id,
            reader.Code,
            reader.SerialNumber,
            controlPoint.Id,
            controlPoint.Code,
            controlPoint.Name
        );
    }
}

using MediatR;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.ControlPoints;

public sealed record CreateControlPointCommand(Guid BusinessUnitId, string Code, string Name, string? Description) : IRequest<Guid>;

public sealed class CreateControlPointHandler(
    IControlPointRepository controlPoints,
    IBusinessUnitRepository businessUnits,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateControlPointCommand, Guid>
{
    public async Task<Guid> Handle(CreateControlPointCommand request, CancellationToken cancellationToken)
    {
        if (!await businessUnits.ExistsAsync(request.BusinessUnitId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(BusinessUnit), request.BusinessUnitId);
        }

        if (await controlPoints.GetByCodeAsync(request.BusinessUnitId, request.Code, cancellationToken) is not null)
        {
            throw new DuplicateException($"Ya existe un punto de control con el código '{request.Code}' para la unidad de negocio.");
        }

        var controlPoint = new ControlPoint(request.Code, request.Name, request.Description, request.BusinessUnitId);
        await controlPoints.AddAsync(controlPoint, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return controlPoint.Id;
    }
}

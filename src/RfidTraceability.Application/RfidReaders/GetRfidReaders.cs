using MediatR;
using RfidTraceability.Application.RfidReaders.Dtos;
using RfidTraceability.Domain.Repositories;

public record GetRfidReadersQuery() : IRequest<List<RfidReaderDto>>;

public class GetRfidReadersHandler
(
    IRfidReaderRepository rfidReaderRepository
) : IRequestHandler<GetRfidReadersQuery, List<RfidReaderDto>>
{
    public async Task<List<RfidReaderDto>> Handle(GetRfidReadersQuery request, CancellationToken cancellationToken)
    {
        var rfidReaders = await rfidReaderRepository.GetAllAsync(cancellationToken);
        return rfidReaders.Select(r => r.ToMap()).ToList();
    }
}

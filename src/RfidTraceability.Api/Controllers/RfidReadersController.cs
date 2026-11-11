using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.RfidReaders;
using RfidTraceability.Application.RfidReaders.Dtos;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class RfidReadersController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateRfidReaderCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Created($"/RfidReaders/{id}", new { id });
    }

    [HttpGet]
    public async Task<ActionResult<List<RfidReaderDto>>> GetAll(CancellationToken cancellationToken)
    {
        var rfidReaders = await sender.Send(new GetRfidReadersQuery(), cancellationToken);
        return Ok(rfidReaders);
    }
}

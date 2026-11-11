using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.RfidReaders;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class RfidReadersController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateRfidReaderCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Created($"/api/rfid-readers/{id}", new { id });
    }
}

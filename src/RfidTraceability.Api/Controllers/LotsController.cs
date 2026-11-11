using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.Lots;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class LotsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateLotCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Created($"/api/lots/{id}", new { id });
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.Blocks;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BlocksController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateBlockCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Created($"/api/blocks/{id}", new { id });
    }
}

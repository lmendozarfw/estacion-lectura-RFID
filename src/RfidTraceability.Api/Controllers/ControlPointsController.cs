using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.ControlPoints;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ControlPointsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateControlPointCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Created($"/api/control-points/{id}", new { id });
    }
}

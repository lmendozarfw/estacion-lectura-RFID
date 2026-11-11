using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.ControlPoints;
using RfidTraceability.Application.ControlPoints.Dtos;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet]
    public async Task<ActionResult<List<ControlPointDto>>> GetAll(CancellationToken cancellationToken)
    {
        var controlPoints = await sender.Send(new GetControlPointsQuery(), cancellationToken);
        return Ok(controlPoints);
    }
}

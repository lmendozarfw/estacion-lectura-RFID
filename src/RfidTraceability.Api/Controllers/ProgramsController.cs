using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.Programs;
using RfidTraceability.Application.Programs.Dtos;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ProgramsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateProgramCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Created($"/api/programs/{id}", new { id });
    }

    [HttpGet]
    public async Task<ActionResult<List<ProgramDto>>> GetAll(CancellationToken cancellationToken)
    {
        var programs = await sender.Send(new GetProgramsQuery(), cancellationToken);
        return Ok(programs);
    }
}

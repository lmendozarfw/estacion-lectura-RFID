using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.Programs;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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
}

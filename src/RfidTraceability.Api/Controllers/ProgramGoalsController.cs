using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.ProgramGoals.Dtos;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ProgramGoalsController(ISender sender) : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateProgramGoalCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Created($"/api/programGoals/{id}", new {id});
    }

    [HttpGet]
    public async Task<ActionResult<List<ProgramGoalDto>>> GetAll(CancellationToken cancellationToken)
    {
        var programGoals = await sender.Send(new GetProgramGoalsQuery(), cancellationToken);
        return Ok(programGoals);
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.ProgramGoals.Dtos;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ProgramGoalsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ProgramGoalDto>>> GetAll(CancellationToken cancellationToken)
    {
        var programGoals = await sender.Send(new GetProgramGoalsQuery(), cancellationToken);
        return Ok(programGoals);
    }
}

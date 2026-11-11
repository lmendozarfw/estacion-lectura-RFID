using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.Organizations;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class OrganizationsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrganizationCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Created($"/Organizations/{id}", new { id });
    }

    [HttpGet]
    public async Task<ActionResult<List<OrganizationDto>>> GetAll(CancellationToken cancellationToken)
    {
        var organizations = await sender.Send(new GetOrganizationsQuery(), cancellationToken);
        return Ok(organizations);
    }
}

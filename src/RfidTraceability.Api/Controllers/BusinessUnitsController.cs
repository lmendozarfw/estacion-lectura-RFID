using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.BusinessUnits;
using RfidTraceability.Application.BusinessUnits.Dtos;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class BusinessUnitsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBusinessUnitCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Created($"/Business-units/{id}", new { id });
    }

    [HttpGet]
    public async Task<ActionResult<List<BusinessUnitDto>>> GetAll(CancellationToken cancellationToken)
    {
        var businessUnits = await sender.Send(new GetBusinessUnitsQuery(), cancellationToken);
        return Ok(businessUnits);
    }
}

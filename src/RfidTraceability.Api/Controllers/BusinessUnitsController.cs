using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.BusinessUnits;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class BusinessUnitsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBusinessUnitCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Created($"/api/business-units/{id}", new { id });
    }
}

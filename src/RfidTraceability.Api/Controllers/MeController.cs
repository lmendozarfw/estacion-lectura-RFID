using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Api.Extensions;
using RfidTraceability.Application.Auth;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public sealed class MeController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<MeResult>> Get(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMeQuery(User.GetUserId()), cancellationToken);
        return Ok(result);
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.Auth;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResult>> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResult>> Refresh([FromBody] RefreshAccessTokenCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Ok(result);
    }
}

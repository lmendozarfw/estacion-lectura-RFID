using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RfidTraceability.Application.Common.Exceptions;

namespace RfidTraceability.Api.Extensions;

/*
Esta clase de extensión proporciona métodos para obtener de forma mas semántica y limpia
los valores de los claims.
¡Vivan los métodos de extensión!
*/
public static class ClaimsPrincipalExtensions
{
    public static Guid GetOrganizationId(this ClaimsPrincipal principal)
    {
        return principal.GetGuidClaim("organizationId", "El token no contiene una organización válida.");
    }

    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        return principal.GetGuidClaim(JwtRegisteredClaimNames.Sub, "El token no contiene un usuario válido.");
    }

    private static Guid GetGuidClaim(this ClaimsPrincipal principal, string claimType, string errorMessage)
    {
        var value = principal.FindFirst(claimType)?.Value;

        if (!Guid.TryParse(value, out var claimValue))
        {
            throw new UnauthorizedException(errorMessage);
        }

        return claimValue;
    }
}

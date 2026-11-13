using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RfidTraceability.Application.Security;

namespace RfidTraceability.Infrastructure.Security;

public sealed class JwtTokenGenerator(IOptions<JwtOptions> options) : IJwtTokenGenerator
{
    public JwtTokenResult Generate(
        Guid userId,
        Guid organizationId,
        string username,
        string employeeId,
        IEnumerable<string> roles)
    {
        var jwt = options.Value;
        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(jwt.ExpirationMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.UniqueName, username),
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Name, username),
            new("organizationId", organizationId.ToString()),
            new("employeeId", employeeId)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            notBefore: now,
            expires: expires,
            signingCredentials: credentials);

        return new JwtTokenResult(new JwtSecurityTokenHandler().WriteToken(token), expires);
    }
}

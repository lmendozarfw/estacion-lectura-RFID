namespace RfidTraceability.Application.Security;

public sealed record JwtTokenResult(string AccessToken, DateTime ExpiresAtUtc);

public interface IJwtTokenGenerator
{
    JwtTokenResult Generate(
        Guid userId,
        Guid organizationId,
        string username,
        string employeeId,
        IEnumerable<string> roles);
}

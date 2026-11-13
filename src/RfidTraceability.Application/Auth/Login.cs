using MediatR;
using Microsoft.Extensions.Options;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Application.Security;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.Auth;

public sealed record LoginCommand(Guid OrganizationId, string Username, string Password) : IRequest<LoginResult>;

public sealed record LoginResult(
    string AccessToken,
    DateTime ExpiresAtUtc,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc);

public sealed class LoginHandler(
    IUserRepository users,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator tokenGenerator,
    IRefreshTokenGenerator refreshTokenGenerator,
    IRefreshTokenRepository refreshTokens,
    IUnitOfWork unitOfWork,
    IOptions<JwtOptions> jwtOptions)
    : IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await users.GetByUsernameAsync(request.OrganizationId, request.Username, cancellationToken);

        if (user is null || !user.Active || !passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Usuario o contraseña inválidos.");
        }

        var roles = user.UserRoles.Select(userRole => userRole.Role.Code);
        var token = tokenGenerator.Generate(
            user.Id,
            user.OrganizationId,
            user.Username,
            user.EmployeeId,
            roles);

        var refreshToken = await CreateRefreshTokenAsync(user.Id, cancellationToken);

        return new LoginResult(
            token.AccessToken,
            token.ExpiresAtUtc,
            refreshToken.RawToken,
            refreshToken.ExpiresAtUtc);
    }

    private async Task<(string RawToken, DateTime ExpiresAtUtc)> CreateRefreshTokenAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var rawToken = refreshTokenGenerator.Generate();
        var expiresAt = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpirationDays);
        var refreshToken = new RefreshToken(userId, refreshTokenGenerator.Hash(rawToken), expiresAt);

        await refreshTokens.AddAsync(refreshToken, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return (rawToken, expiresAt);
    }
}

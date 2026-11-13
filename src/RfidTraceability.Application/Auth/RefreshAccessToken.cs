using MediatR;
using Microsoft.Extensions.Options;
using RfidTraceability.Application.Common;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Application.Security;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.Auth;

public sealed record RefreshAccessTokenCommand(string RefreshToken) : IRequest<LoginResult>;

public sealed class RefreshAccessTokenHandler(
    IRefreshTokenRepository refreshTokens,
    IRefreshTokenGenerator refreshTokenGenerator,
    IJwtTokenGenerator tokenGenerator,
    IUnitOfWork unitOfWork,
    IOptions<JwtOptions> jwtOptions)
    : IRequestHandler<RefreshAccessTokenCommand, LoginResult>
{
    public async Task<LoginResult> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var tokenHash = refreshTokenGenerator.Hash(request.RefreshToken);
        var storedToken = await refreshTokens.GetByTokenHashAsync(tokenHash, cancellationToken);

        if (storedToken is null || !storedToken.IsActive)
        {
            throw new UnauthorizedException("El refresh token es inválido o expiró.");
        }

        var user = storedToken.User;
        if (!user.Active)
        {
            throw new UnauthorizedException("El usuario está inactivo.");
        }

        var roles = user.UserRoles.Select(userRole => userRole.Role.Code);
        var accessToken = tokenGenerator.Generate(
            user.Id,
            user.OrganizationId,
            user.Username,
            user.EmployeeId,
            roles);

        var newRawToken = refreshTokenGenerator.Generate();
        var newExpiresAt = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpirationDays);
        var newRefreshToken = new RefreshToken(user.Id, refreshTokenGenerator.Hash(newRawToken), newExpiresAt);

        storedToken.Revoke(newRefreshToken.TokenHash);
        refreshTokens.Update(storedToken);
        await refreshTokens.AddAsync(newRefreshToken, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginResult(
            accessToken.AccessToken,
            accessToken.ExpiresAtUtc,
            newRawToken,
            newExpiresAt);
    }
}

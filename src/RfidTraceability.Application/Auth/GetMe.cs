using MediatR;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.Auth;

public sealed record GetMeQuery(Guid UserId) : IRequest<MeResult>;

public sealed record MeResult(
    Guid Id,
    string Username,
    string EmployeeId,
    bool Active,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    MeOrganizationResult Organization,
    IReadOnlyList<string> Roles);

public sealed record MeOrganizationResult(
    Guid Id,
    string Code,
    string Name,
    bool Active);

public sealed class GetMeHandler(IUserRepository users) : IRequestHandler<GetMeQuery, MeResult>
{
    public async Task<MeResult> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var user = await users.GetWithOrganizationAsync(request.UserId, cancellationToken)
            ?? throw new EntityNotFoundException("Usuario", request.UserId);

        return new MeResult(
            user.Id,
            user.Username,
            user.EmployeeId,
            user.Active,
            user.CreatedAt,
            user.UpdatedAt,
            new MeOrganizationResult(
                user.Organization.Id,
                user.Organization.Code,
                user.Organization.Name,
                user.Organization.Active),
            user.UserRoles.Select(userRole => userRole.Role.Code).ToList());
    }
}

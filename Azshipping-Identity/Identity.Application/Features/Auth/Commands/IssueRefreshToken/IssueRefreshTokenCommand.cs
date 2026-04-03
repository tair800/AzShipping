using Identity.Application.DTOs.Auth;
using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Auth.Commands.IssueRefreshToken;

public sealed record IssueRefreshTokenCommand : IRequest<RefreshTokenIssueResultDto>, ITransactionalRequest
{
    public long UserId { get; init; }

    public IssueRefreshTokenCommand(long? userId)
    {
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
    }
}
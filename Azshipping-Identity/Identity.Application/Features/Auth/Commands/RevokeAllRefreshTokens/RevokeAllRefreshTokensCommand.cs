using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Auth.Commands.RevokeAllRefreshTokens;

public sealed record RevokeAllRefreshTokensCommand : IRequest, ITransactionalRequest
{
    public long UserId { get; init; }

    public RevokeAllRefreshTokensCommand(long? userId)
    {
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
    }
}
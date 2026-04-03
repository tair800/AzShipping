using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.Features.Auth.Commands.RevokeAllRefreshTokens;

public sealed class RevokeAllRefreshTokensCommandHandler(IRefreshTokenService refreshTokenService) : IRequestHandler<RevokeAllRefreshTokensCommand>
{
    private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;

    public async Task Handle(RevokeAllRefreshTokensCommand request, CancellationToken cancellationToken)
    {
        await _refreshTokenService.RevokeAllAsync(request.UserId, cancellationToken);
    }
}
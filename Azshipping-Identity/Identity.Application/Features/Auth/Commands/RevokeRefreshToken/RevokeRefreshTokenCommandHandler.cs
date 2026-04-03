using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.Features.Auth.Commands.RevokeRefreshToken;

public sealed class RevokeRefreshTokenCommandHandler(IRefreshTokenService refreshTokenService) : IRequestHandler<RevokeRefreshTokenCommand>
{
    private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;
    public async Task Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        await _refreshTokenService.RevokeAsync(request.RevokeRefreshTokenRequestDto, cancellationToken);
    }
}
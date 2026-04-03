using Identity.Application.DTOs.Auth;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler(IRefreshTokenService refreshTokenService) : IRequestHandler<RefreshTokenCommand, RefreshResultDto>
{
    private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;

    public async Task<RefreshResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _refreshTokenService.RefreshAsync(request.RefreshToken, cancellationToken);
    }
}
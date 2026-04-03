using Identity.Application.DTOs.Auth;
using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Auth.Commands.RevokeRefreshToken;

public sealed record RevokeRefreshTokenCommand : IRequest, ITransactionalRequest
{
    public RevokeRefreshTokenRequestDto RevokeRefreshTokenRequestDto { get; init; }

    public RevokeRefreshTokenCommand(RevokeRefreshTokenRequestDto revokeRefreshTokenRequestDto)
    {
        RevokeRefreshTokenRequestDto = revokeRefreshTokenRequestDto ?? throw new ArgumentNullException(nameof(revokeRefreshTokenRequestDto));
    }
}
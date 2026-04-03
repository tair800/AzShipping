using Identity.Application.DTOs.Auth;
using MediatR;

namespace Identity.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand : IRequest<RefreshResultDto>
{
    public string RefreshToken { get; init; }

    public RefreshTokenCommand(string refreshToken)
    {
        RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
    }
}
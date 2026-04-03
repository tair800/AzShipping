using Identity.Application.DTOs.Auth;
using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand : IRequest<AccessTokenDto>, ITransactionalRequest
{
    public LoginDto LoginDto { get; init; }

    public LoginCommand(LoginDto loginDto)
    {
        LoginDto = loginDto ?? throw new ArgumentNullException(nameof(loginDto));
    }
}
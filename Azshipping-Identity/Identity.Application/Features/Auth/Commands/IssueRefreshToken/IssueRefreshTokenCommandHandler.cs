using Identity.Application.DTOs.Auth;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.Features.Auth.Commands.IssueRefreshToken;

public sealed class IssueRefreshTokenCommandHandler(IRefreshTokenService refreshTokenService) : IRequestHandler<IssueRefreshTokenCommand, RefreshTokenIssueResultDto>
{
    private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;

    public async Task<RefreshTokenIssueResultDto> Handle(IssueRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _refreshTokenService.IssueAsync(request.UserId, cancellationToken);
    }
}
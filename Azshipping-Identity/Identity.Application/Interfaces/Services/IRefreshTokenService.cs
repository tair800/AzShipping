using Identity.Application.DTOs.Auth;

namespace Identity.Application.Interfaces.Services;

public interface IRefreshTokenService
{
    Task<RefreshTokenIssueResultDto> IssueAsync(long userId, CancellationToken cancellationToken = default);
    Task<RefreshResultDto> RefreshAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task RevokeAsync(RevokeRefreshTokenRequestDto dto, CancellationToken cancellationToken = default);
    Task RevokeAllAsync(long userId, CancellationToken cancellationToken = default);
}
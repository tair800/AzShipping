using Identity.Application.DTOs.Auth;
using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.RefreshTokenAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MrStyx.Application.Exceptions;
using MrStyx.Application.Interfaces;
using MrStyx.Domain.SeedWork.Persistence;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Infrastructure.Services;

public class RefreshTokenService
(
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository,
    ITokenService tokenService,
    IUnitOfWork unitOfWork,
    IRequestContext requestContext,
    IPermissionReadService permissionReadService,
    IEmployeeGroupPermissionClaimsService employeeGroupPermissionClaimsService,
    IOptions<RefreshTokenOptions> refreshTokenOptions,
    IOptions<PepperOptions> pepperOptions

) : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IRequestContext _requestContext = requestContext;
    private readonly IPermissionReadService _permissionReadService = permissionReadService;
    private readonly IEmployeeGroupPermissionClaimsService _employeeGroupPermissionClaimsService = employeeGroupPermissionClaimsService;
    private readonly RefreshTokenOptions _refreshTokenOptions = refreshTokenOptions.Value;
    private readonly PepperOptions _pepperOptions = pepperOptions.Value;

    public async Task<RefreshTokenIssueResultDto> IssueAsync(long userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new NotFoundException("User not found");

        var now = DateTime.UtcNow;
        var expiresAt = now.AddDays(_refreshTokenOptions.LifeTimeDays);

        var rawToken = GenerateRawToken();
        var tokenHash = ComputeTokenHash(rawToken);

        var refreshToken = RefreshToken.Create(userId, tokenHash, now, expiresAt, _requestContext.ClientIp, _requestContext.UserAgent);

        await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RefreshTokenIssueResultDto(rawToken, expiresAt);
    }

    public async Task<RefreshResultDto> RefreshAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(refreshToken)) throw new UnauthorizedException("Invalid refresh token");

        var tokenHash = ComputeTokenHash(refreshToken);

        var token = await _refreshTokenRepository.GetFirstOrDefaultAsync(t => t.TokenHash == tokenHash, cancellationToken, trackingMode: QueryTrackingMode.Tracking)
            ?? throw new UnauthorizedException("Invalid refresh token");

        var now = DateTime.UtcNow;

        if (token.RevokedAtUtc is not null)
        {
            if (_refreshTokenOptions.RevokeDescendantsOnReuse) await RevokeChainByHashAsync(token.ReplacedByTokenHash, _requestContext.ClientIp, cancellationToken);

            throw new UnauthorizedException("Refresh token revoked");
        }

        if (token.ExpiresAtUtc <= now) throw new UnauthorizedException("Refresh token expired");

        var user = await _userRepository.GetByIdAsync(token.UserId, cancellationToken) ?? throw new UnauthorizedException("User not found");

        var roles = await _permissionReadService.GetUserRolesAsync(user.Id);

        var permissions = await _permissionReadService.GetUserPermissionsAsync(user.Id);

        var erp = await _employeeGroupPermissionClaimsService.ResolveAsync(user.EmployeeGroupIds, user.UnlimitedAccess, cancellationToken);

        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Username.Value, user.Email.Value, roles, permissions, erp);

        if (_refreshTokenOptions.RotateOnUse)
        {
            var newRaw = GenerateRawToken();
            var newHash = ComputeTokenHash(newRaw);
            var newExpiresAt = now.AddDays(_refreshTokenOptions.LifeTimeDays);

            var newRefreshToken = RefreshToken.Create(user.Id, newHash, now, newExpiresAt, _requestContext.ClientIp, _requestContext.UserAgent);

            await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);

            token.UpdateRevokeData(now, _requestContext.ClientIp, newHash);

            _refreshTokenRepository.Update(token);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RefreshResultDto(accessToken, newRaw, newExpiresAt);
        }

        return new RefreshResultDto(accessToken, refreshToken, token.ExpiresAtUtc);
    }

    public async Task RevokeAsync(RevokeRefreshTokenRequestDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.RefreshToken)) return;

        var tokenHash = ComputeTokenHash(dto.RefreshToken);
        var token = await _refreshTokenRepository.GetFirstOrDefaultAsync(t => t.TokenHash == tokenHash, cancellationToken, trackingMode: QueryTrackingMode.Tracking);

        if (token is null) return;

        var now = DateTime.UtcNow;

        if (token.RevokedAtUtc is null)
        {
            token.UpdateRevokeData(now, _requestContext.ClientIp);
            _refreshTokenRepository.Update(token);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        if (dto.RevokeFamily)
        {
            await RevokeChainByHashAsync(token.ReplacedByTokenHash, _requestContext.ClientIp, cancellationToken);
        }
    }

    public async Task RevokeAllAsync(long userId, CancellationToken cancellationToken = default)
    {
        var activeTokens = await _refreshTokenRepository.GetWhereAsync(t => t.UserId == userId && t.RevokedAtUtc == null, cancellationToken, trackingMode: QueryTrackingMode.Tracking);

        if (activeTokens.Count == 0) return;

        var now = DateTime.UtcNow;

        foreach (var token in activeTokens)
        {
            token.UpdateRevokeData(now, _requestContext.ClientIp);
            _refreshTokenRepository.Update(token);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task RevokeChainByHashAsync(string? startTokenHash, string? ip, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(startTokenHash)) return;

        var currentHash = startTokenHash;
        var now = DateTime.UtcNow;

        while (!string.IsNullOrWhiteSpace(currentHash))
        {
            var node = await _refreshTokenRepository.GetFirstOrDefaultAsync(t => t.TokenHash == currentHash, cancellationToken, trackingMode: QueryTrackingMode.Tracking);

            if (node is null) break;

            if (node.RevokedAtUtc is null)
            {
                node.UpdateRevokeData(now, ip);
                _refreshTokenRepository.Update(node);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            currentHash = node.ReplacedByTokenHash;
        }
    }

    private static string GenerateRawToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Base64UrlEncode(bytes);
    }

    private string ComputeTokenHash(string rawToken)
    {
        using var hmac = new HMACSHA256(_pepperOptions.Pepper);
        var bytes = Encoding.UTF8.GetBytes(rawToken);
        var hash = hmac.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }

    private static string Base64UrlEncode(byte[] bytes)
    {
        return Convert.ToBase64String(bytes)
                      .TrimEnd('=')
                      .Replace('+', '-')
                      .Replace('/', '_');
    }
}
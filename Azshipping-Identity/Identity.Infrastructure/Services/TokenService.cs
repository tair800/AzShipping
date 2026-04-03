using Identity.Application.DTOs.Auth;
using Identity.Application.Interfaces.Services;
using Identity.Application.Services;
using Identity.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Infrastructure.Services;

public class TokenService(IOptions<JwtOptions> jwtOptions, IConfiguration configuration) : ITokenService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly SymmetricSecurityKey _signingKey = CreateSigningKey(configuration["JWT:SecretKey"]);
    public AccessTokenDto GenerateAccessToken(
        long userId,
        string username,
        string email,
        IReadOnlyCollection<string> roles,
        IReadOnlyCollection<string> permissions,
        ErpPermissionResolution erp)
    {
        var now = DateTime.UtcNow;
        var expiresAt = now.AddMinutes(_jwtOptions.AccessTokenLifetimeMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new(JwtRegisteredClaimNames.Email, email),
            new("uid", userId.ToString())
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        claims.AddRange(permissions.Select(p => new Claim("permission", p)));

        if (erp.Unlimited)
            claims.Add(new Claim(ErpClaimTypes.Unlimited, "1"));
        else
            claims.AddRange(erp.Claims.Select(c => new Claim(ErpClaimTypes.Permission, c)));

        var creds = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken
            (
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: expiresAt,
                signingCredentials: creds
            );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new AccessTokenDto
        {
            Token = token,
            IssuedAtUtc = now,
            ExpiresAtUtc = expiresAt
        };
    }

    private static SymmetricSecurityKey CreateSigningKey(string? secret)
    {
        if (string.IsNullOrWhiteSpace(secret))
            throw new InvalidOperationException("JWT:SecretKey is required (use user-secrets/ENV/KeyVault).");

        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    }
}

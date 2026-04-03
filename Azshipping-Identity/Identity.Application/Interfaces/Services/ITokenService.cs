using Identity.Application.DTOs.Auth;

namespace Identity.Application.Interfaces.Services;

public interface ITokenService
{
    AccessTokenDto GenerateAccessToken(
        long userId,
        string username,
        string email,
        IReadOnlyCollection<string> roles,
        IReadOnlyCollection<string> permissions,
        ErpPermissionResolution erp);
}
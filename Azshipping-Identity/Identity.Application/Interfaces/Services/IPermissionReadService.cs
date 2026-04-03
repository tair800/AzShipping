namespace Identity.Application.Interfaces.Services;

public interface IPermissionReadService
{
    Task<bool> UserHasPermissionAsync(long userId, string permission);
    Task<IReadOnlyCollection<string>> GetUserPermissionsAsync(long userId);
    Task<IReadOnlyCollection<string>> GetUserRolesAsync(long userId);
}
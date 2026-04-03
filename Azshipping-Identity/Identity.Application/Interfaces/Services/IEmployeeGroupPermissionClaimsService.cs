namespace Identity.Application.Interfaces.Services;

public readonly record struct ErpPermissionResolution(IReadOnlyList<string> Claims, bool Unlimited);

/// <summary>Loads merged ERP permission claim strings from Settings using the user's employee group ids.</summary>
public interface IEmployeeGroupPermissionClaimsService
{
    Task<ErpPermissionResolution> ResolveAsync(
        IReadOnlyList<Guid> employeeGroupIds,
        bool unlimitedAccess,
        CancellationToken cancellationToken = default);
}

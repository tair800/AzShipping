using Identity.Application.DTOs.User;

namespace Identity.Application.Interfaces.Services;

public interface ILicensingService
{
    Task EnsureCanActivateAnotherUserAsync(CancellationToken cancellationToken);

    Task<UserLicenseStatsDto> GetStatsAsync(CancellationToken cancellationToken);
}

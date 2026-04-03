using Identity.Application.DTOs.User;
using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Domain.AggregatesModel.UserAggregate.Enumerations;
using Identity.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MrStyx.Application.Exceptions;

namespace Identity.Infrastructure.Services;

public sealed class LicensingService(
    IUserRepository userRepository,
    IOptions<LicensingOptions> options) : ILicensingService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly LicensingOptions _options = options.Value;

    public async Task EnsureCanActivateAnotherUserAsync(CancellationToken cancellationToken)
    {
        var max = _options.MaxActivatedUsers;
        if (!max.HasValue || max.Value <= 0)
            return;

        var activeUsers = await _userRepository.GetWhereAsync(u => u.Status == UserStatus.Active, cancellationToken);
        if (activeUsers.Count >= max.Value)
            throw new ConflictException($"Activated user limit reached ({max.Value} licenses in use).");
    }

    public async Task<UserLicenseStatsDto> GetStatsAsync(CancellationToken cancellationToken)
    {
        var activeUsers = await _userRepository.GetWhereAsync(u => u.Status == UserStatus.Active, cancellationToken);
        var activated = activeUsers.Count;
        var max = _options.MaxActivatedUsers;
        int? free = max.HasValue ? Math.Max(0, max.Value - activated) : null;
        return new UserLicenseStatsDto(activated, max, free);
    }
}

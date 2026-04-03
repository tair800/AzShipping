using Identity.Application.DTOs.User;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetLicenseStats;

public sealed class GetUserLicenseStatsQueryHandler(ILicensingService licensingService)
    : IRequestHandler<GetUserLicenseStatsQuery, UserLicenseStatsDto>
{
    public Task<UserLicenseStatsDto> Handle(GetUserLicenseStatsQuery request, CancellationToken cancellationToken)
        => licensingService.GetStatsAsync(cancellationToken);
}

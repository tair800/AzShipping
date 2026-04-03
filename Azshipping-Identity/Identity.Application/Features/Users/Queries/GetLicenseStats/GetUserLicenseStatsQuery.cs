using Identity.Application.DTOs.User;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetLicenseStats;

public sealed record GetUserLicenseStatsQuery : IRequest<UserLicenseStatsDto>;

using Identity.Domain.AggregatesModel.UserAggregate.Enumerations;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetStatus;

public record GetUserStatusesQuery() : IRequest<IReadOnlyCollection<UserStatus>>;
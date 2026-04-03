using Identity.Application.DTOs.User;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetById;

public sealed record GetUserByIdQuery(long Id) : IRequest<UserDto>;

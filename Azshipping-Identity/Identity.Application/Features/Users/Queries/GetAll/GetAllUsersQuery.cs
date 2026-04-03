using Identity.Application.DTOs.User;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetAll;

public sealed record GetAllUsersQuery() : IRequest<UserList>;
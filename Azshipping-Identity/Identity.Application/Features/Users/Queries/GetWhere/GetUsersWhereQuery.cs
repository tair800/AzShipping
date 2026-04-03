using Identity.Application.DTOs.User;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetWhere;

public sealed record GetUsersWhereQuery(SearchUserDto SearchUserDto) : IRequest<UserList>;
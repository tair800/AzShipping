using Identity.Application.DTOs.User;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetPagedWhere;

public sealed record GetPagedUsersWhereQuery(int PageNumber, int PageSize, SearchUserDto SearchUserDto) : IRequest<PagedUserList>;
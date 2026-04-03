using Identity.Application.DTOs.User;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetPaged;

public sealed record GetPagedUsersQuery(int PageNumber, int PageSize) : IRequest<PagedUserList>;
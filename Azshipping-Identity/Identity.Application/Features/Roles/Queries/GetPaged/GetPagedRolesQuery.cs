using Identity.Application.DTOs.Role;
using MediatR;

namespace Identity.Application.Features.Roles.Queries.GetPaged;

public sealed record GetPagedRolesQuery(int PageNumber, int PageSize) : IRequest<PagedRoleList>;
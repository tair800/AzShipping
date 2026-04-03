using Identity.Application.DTOs.Role;
using MediatR;

namespace Identity.Application.Features.Roles.Queries.GetPagedWhere;

public sealed record GetPagedRolesWhereQuery(int PageNumber, int PageSize, SearchRoleDto SearchRoleDto) : IRequest<PagedRoleList>;
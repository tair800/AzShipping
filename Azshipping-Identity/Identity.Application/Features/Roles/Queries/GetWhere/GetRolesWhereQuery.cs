using Identity.Application.DTOs.Role;
using MediatR;

namespace Identity.Application.Features.Roles.Queries.GetWhere;

public sealed record GetRolesWhereQuery(SearchRoleDto SearchRoleDto) : IRequest<RoleList>;
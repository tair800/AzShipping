using Identity.Application.DTOs.Role;
using MediatR;

namespace Identity.Application.Features.Roles.Queries.GetById;

public sealed record GetRoleByIdQuery(long Id) : IRequest<RoleDto>;
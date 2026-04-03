using Identity.Application.DTOs.Role;
using MediatR;

namespace Identity.Application.Features.Roles.Queries.GetAll;

public sealed record GetAllRolesQuery() : IRequest<RoleList>;
using Identity.Application.DTOs.Permission;
using MediatR;

namespace Identity.Application.Features.Permissions.Queries.GetAll;

public sealed record GetAllPermissionsQuery() : IRequest<PermissionList>;
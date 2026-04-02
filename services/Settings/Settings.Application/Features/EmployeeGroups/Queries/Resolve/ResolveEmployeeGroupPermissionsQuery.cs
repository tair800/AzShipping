using MediatR;
using Settings.Application.DTOs.EmployeeGroup;

namespace Settings.Application.Features.EmployeeGroups.Queries.Resolve;

public sealed record ResolveEmployeeGroupPermissionsQuery(IReadOnlyList<Guid> Ids)
    : IRequest<ResolveEmployeeGroupPermissionsResponseDto>;

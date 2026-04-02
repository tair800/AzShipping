using MediatR;
using Settings.Application.DTOs.EmployeeGroup;

namespace Settings.Application.Features.EmployeeGroups.Queries.GetAll;

public sealed record GetAllEmployeeGroupsQuery(Guid? CompanyId = null, string? Search = null) : IRequest<IReadOnlyList<EmployeeGroupListItemDto>>;

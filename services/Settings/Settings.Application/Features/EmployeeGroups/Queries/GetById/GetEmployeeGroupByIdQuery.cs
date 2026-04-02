using MediatR;
using Settings.Application.DTOs.EmployeeGroup;

namespace Settings.Application.Features.EmployeeGroups.Queries.GetById;

public sealed record GetEmployeeGroupByIdQuery(Guid Id) : IRequest<EmployeeGroupDetailDto?>;

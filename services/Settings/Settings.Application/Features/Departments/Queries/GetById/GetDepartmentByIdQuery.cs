using MediatR;
using Settings.Application.DTOs.Department;

namespace Settings.Application.Features.Departments.Queries.GetById;

public sealed record GetDepartmentByIdQuery(Guid Id) : IRequest<DepartmentDto?>;

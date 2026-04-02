using MediatR;
using Settings.Application.DTOs.Department;

namespace Settings.Application.Features.Departments.Queries.GetAll;

public sealed record GetAllDepartmentsQuery : IRequest<IReadOnlyList<DepartmentDto>>;

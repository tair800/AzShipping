using General.Application.DTOs.Employee;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetAll;

public record GetAllEmployeesQuery : IRequest<IReadOnlyList<EmployeeDto>>;

using General.Application.DTOs.Employee;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetById;

public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto?>;

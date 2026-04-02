using General.Application.DTOs.Employee;
using MediatR;

namespace General.Application.Features.Employees.Commands.Create;

public record CreateEmployeeCommand(CreateEmployeeDto Dto) : IRequest<EmployeeDto>;

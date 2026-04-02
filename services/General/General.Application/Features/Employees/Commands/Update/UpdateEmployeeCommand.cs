using General.Application.DTOs.Employee;
using MediatR;

namespace General.Application.Features.Employees.Commands.Update;

public record UpdateEmployeeCommand(Guid Id, UpdateEmployeeDto Dto) : IRequest<EmployeeDto?>;

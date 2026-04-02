using General.Application.DTOs.Employee;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetByUserId;

public record GetEmployeeByUserIdQuery(long UserId) : IRequest<EmployeeDto?>;

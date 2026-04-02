using MediatR;

namespace General.Application.Features.Employees.Commands.Delete;

public record DeleteEmployeeCommand(Guid Id) : IRequest<bool>;

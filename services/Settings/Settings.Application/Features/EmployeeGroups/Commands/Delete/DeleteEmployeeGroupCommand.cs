using MediatR;

namespace Settings.Application.Features.EmployeeGroups.Commands.Delete;

public sealed record DeleteEmployeeGroupCommand(Guid Id) : IRequest<bool>;

using MediatR;

namespace Settings.Application.Features.TaskPriorities.Commands.Delete;

public sealed record DeleteTaskPriorityCommand(Guid Id) : IRequest<bool>;

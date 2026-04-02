using MediatR;

namespace Settings.Application.Features.TaskStatuses.Commands.Delete;

public sealed record DeleteTaskStatusCommand(Guid Id) : IRequest<bool>;

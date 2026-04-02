using MediatR;
using Settings.Application.DTOs.TaskStatus;

namespace Settings.Application.Features.TaskStatuses.Commands.Update;

public sealed record UpdateTaskStatusCommand(Guid Id, UpdateTaskStatusDto Dto) : IRequest<TaskStatusDto?>;

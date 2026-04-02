using MediatR;
using Settings.Application.DTOs.TaskStatus;

namespace Settings.Application.Features.TaskStatuses.Commands.Create;

public sealed record CreateTaskStatusCommand(CreateTaskStatusDto Dto) : IRequest<TaskStatusDto>;

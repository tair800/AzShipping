using MediatR;
using Settings.Application.DTOs.TaskPriority;

namespace Settings.Application.Features.TaskPriorities.Commands.Create;

public sealed record CreateTaskPriorityCommand(CreateTaskPriorityDto Dto) : IRequest<TaskPriorityDto>;

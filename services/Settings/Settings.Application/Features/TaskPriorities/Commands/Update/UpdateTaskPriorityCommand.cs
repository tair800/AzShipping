using MediatR;
using Settings.Application.DTOs.TaskPriority;

namespace Settings.Application.Features.TaskPriorities.Commands.Update;

public sealed record UpdateTaskPriorityCommand(Guid Id, UpdateTaskPriorityDto Dto) : IRequest<TaskPriorityDto?>;

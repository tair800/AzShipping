using MediatR;
using Settings.Application.DTOs.TaskPriority;

namespace Settings.Application.Features.TaskPriorities.Queries.GetAll;

public sealed record GetAllTaskPrioritiesQuery : IRequest<IReadOnlyList<TaskPriorityDto>>;

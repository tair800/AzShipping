using MediatR;
using Settings.Application.DTOs.TaskPriority;

namespace Settings.Application.Features.TaskPriorities.Queries.GetById;

public sealed record GetTaskPriorityByIdQuery(Guid Id) : IRequest<TaskPriorityDto?>;

using MediatR;
using Settings.Application.DTOs.TaskStatus;

namespace Settings.Application.Features.TaskStatuses.Queries.GetById;

public sealed record GetTaskStatusByIdQuery(Guid Id) : IRequest<TaskStatusDto?>;

using MediatR;
using Settings.Application.DTOs.TaskStatus;

namespace Settings.Application.Features.TaskStatuses.Queries.GetAll;

public sealed record GetAllTaskStatusesQuery : IRequest<IReadOnlyList<TaskStatusDto>>;

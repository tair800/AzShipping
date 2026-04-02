using General.Application.DTOs.Task;
using MediatR;

namespace General.Application.Features.Tasks.Queries.GetAll;

public record GetAllTasksQuery : IRequest<IReadOnlyList<TaskDto>>;

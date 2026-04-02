using General.Application.DTOs.Task;
using MediatR;

namespace General.Application.Features.Tasks.Queries.GetById;

public record GetTaskByIdQuery(Guid Id) : IRequest<TaskDto?>;

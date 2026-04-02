using General.Application.DTOs.Task;
using MediatR;

namespace General.Application.Features.Tasks.Queries.GetByOperation;

public sealed record GetTasksByOperationQuery(Guid OperationId) : IRequest<IReadOnlyList<TaskDto>>;

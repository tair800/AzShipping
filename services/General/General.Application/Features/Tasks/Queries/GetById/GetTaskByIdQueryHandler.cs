using General.Application.DTOs.Task;
using General.Application.Features.Tasks;
using General.Domain.AggregatesModel.TaskAggregate;
using MediatR;

namespace General.Application.Features.Tasks.Queries.GetById;

public class GetTaskByIdQueryHandler(ITaskRepository repository)
    : IRequestHandler<GetTaskByIdQuery, TaskDto?>
{
    public async System.Threading.Tasks.Task<TaskDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : TaskMapper.MapToDto(entity);
    }
}

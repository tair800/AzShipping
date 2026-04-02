using MediatR;
using Settings.Domain.AggregatesModel.ExecutionPlaceAggregate;

namespace Settings.Application.Features.ExecutionPlaces.Commands.Delete;

public sealed class DeleteExecutionPlaceCommandHandler(IExecutionPlaceRepository repository) : IRequestHandler<DeleteExecutionPlaceCommand, bool>
{
    public async Task<bool> Handle(DeleteExecutionPlaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}

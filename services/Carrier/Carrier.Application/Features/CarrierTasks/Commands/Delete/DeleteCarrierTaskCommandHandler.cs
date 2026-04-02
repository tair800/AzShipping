using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierTasks.Commands.Delete;

public class DeleteCarrierTaskCommandHandler(ITaskRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteCarrierTaskCommand, bool>
{
    public async Task<bool> Handle(DeleteCarrierTaskCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;
        var title = existing.TaskName;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Carrier task deleted", $"carrier task: {title} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}

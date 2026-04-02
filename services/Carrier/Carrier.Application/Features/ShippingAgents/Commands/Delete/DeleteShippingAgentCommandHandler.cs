using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.ShippingAgentAggregate;
using MediatR;

namespace Carrier.Application.Features.ShippingAgents.Commands.Delete;

public class DeleteShippingAgentCommandHandler(IShippingAgentRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteShippingAgentCommand, bool>
{
    public async Task<bool> Handle(DeleteShippingAgentCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;
        var name = existing.CompanyName;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Shipping agent deleted", $"shipping agent: {name} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}

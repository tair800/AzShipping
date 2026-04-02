using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.ShippingLineAggregate;
using MediatR;

namespace Carrier.Application.Features.ShippingLines.Commands.Delete;

public class DeleteShippingLineCommandHandler(IShippingLineRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteShippingLineCommand, bool>
{
    public async Task<bool> Handle(DeleteShippingLineCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;
        var name = existing.Name;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Shipping line deleted", $"shipping line: {name} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}

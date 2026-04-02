using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierDocuments.Commands.Delete;

public class DeleteCarrierDocumentCommandHandler(ICarrierDocumentRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteCarrierDocumentCommand, bool>
{
    public async Task<bool> Handle(DeleteCarrierDocumentCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;
        var name = existing.DocumentName;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Carrier document deleted", $"carrier document: {name} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}

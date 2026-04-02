using Clients.Application.Services;
using Clients.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace Clients.Application.Features.Documents.Commands.Delete;

public sealed class DeleteDocumentCommandHandler(IDocumentRepository repository, IActionLogClient actionLogClient) : IRequestHandler<DeleteDocumentCommand, bool>
{
    public async Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        var name = entity.DocumentName;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Client document deleted", $"document: {name} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}

using Clients.Application.Services;
using Clients.Domain.AggregatesModel.ClientAggregate;
using MediatR;

namespace Clients.Application.Features.Clients.Commands.Delete;

public sealed class DeleteClientCommandHandler(IClientRepository repository, IActionLogClient actionLogClient) : IRequestHandler<DeleteClientCommand, bool>
{
    public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        var name = entity.CompanyName;
        var code = entity.Code;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Client deleted", $"client: {name} • code: {code} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}

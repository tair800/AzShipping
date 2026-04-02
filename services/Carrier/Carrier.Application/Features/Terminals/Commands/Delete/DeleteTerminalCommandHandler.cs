using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.TerminalAggregate;
using MediatR;

namespace Carrier.Application.Features.Terminals.Commands.Delete;

public sealed class DeleteTerminalCommandHandler(ITerminalRepository repository, IActionLogClient actionLogClient) : IRequestHandler<DeleteTerminalCommand, bool>
{
    public async Task<bool> Handle(DeleteTerminalCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;
        var name = existing.Name;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Terminal deleted", $"terminal: {name} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}

using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Commands.Delete;

public sealed class DeleteVatDefinitionCommandHandler(IVatDefinitionRepository repository)
    : IRequestHandler<DeleteVatDefinitionCommand, bool>
{
    public async Task<bool> Handle(DeleteVatDefinitionCommand request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (e == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}

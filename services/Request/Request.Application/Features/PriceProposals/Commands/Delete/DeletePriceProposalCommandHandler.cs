using MediatR;
using Request.Domain.AggregatesModel.PriceProposalAggregate;

namespace Request.Application.Features.PriceProposals.Commands.Delete;

public sealed class DeletePriceProposalCommandHandler(IPriceProposalRepository repository)
    : IRequestHandler<DeletePriceProposalCommand, bool>
{
    public async Task<bool> Handle(DeletePriceProposalCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}

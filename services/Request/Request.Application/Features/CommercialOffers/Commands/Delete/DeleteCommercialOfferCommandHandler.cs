using MediatR;
using Request.Domain.AggregatesModel.CommercialOfferAggregate;

namespace Request.Application.Features.CommercialOffers.Commands.Delete;

public sealed class DeleteCommercialOfferCommandHandler(ICommercialOfferRepository repository)
    : IRequestHandler<DeleteCommercialOfferCommand, bool>
{
    public async Task<bool> Handle(DeleteCommercialOfferCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null)
            return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}

using MediatR;
using Request.Application.DTOs.CommercialOffer;
using Request.Application.Features.CommercialOffers;
using Request.Domain.AggregatesModel.CommercialOfferAggregate;

namespace Request.Application.Features.CommercialOffers.Queries.GetById;

public sealed class GetCommercialOfferByIdQueryHandler(ICommercialOfferRepository repository)
    : IRequestHandler<GetCommercialOfferByIdQuery, CommercialOfferDto?>
{
    public async Task<CommercialOfferDto?> Handle(GetCommercialOfferByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : CommercialOfferMapper.MapToDto(entity);
    }
}

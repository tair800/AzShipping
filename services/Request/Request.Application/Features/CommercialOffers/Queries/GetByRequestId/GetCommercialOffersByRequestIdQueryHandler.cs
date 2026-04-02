using MediatR;
using Request.Application.DTOs.CommercialOffer;
using Request.Application.Features.CommercialOffers;
using Request.Domain.AggregatesModel.CommercialOfferAggregate;

namespace Request.Application.Features.CommercialOffers.Queries.GetByRequestId;

public sealed class GetCommercialOffersByRequestIdQueryHandler(ICommercialOfferRepository repository)
    : IRequestHandler<GetCommercialOffersByRequestIdQuery, IReadOnlyList<CommercialOfferDto>>
{
    public async Task<IReadOnlyList<CommercialOfferDto>> Handle(GetCommercialOffersByRequestIdQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetByRequestIdAsync(request.RequestId, cancellationToken);
        return list.Select(CommercialOfferMapper.MapToDto).ToList();
    }
}

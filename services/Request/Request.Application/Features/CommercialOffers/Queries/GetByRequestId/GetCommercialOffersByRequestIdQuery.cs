using MediatR;
using Request.Application.DTOs.CommercialOffer;

namespace Request.Application.Features.CommercialOffers.Queries.GetByRequestId;

public sealed record GetCommercialOffersByRequestIdQuery(Guid RequestId) : IRequest<IReadOnlyList<CommercialOfferDto>>;

using MediatR;
using Request.Application.DTOs.CommercialOffer;

namespace Request.Application.Features.CommercialOffers.Queries.GetById;

public sealed record GetCommercialOfferByIdQuery(Guid Id) : IRequest<CommercialOfferDto?>;

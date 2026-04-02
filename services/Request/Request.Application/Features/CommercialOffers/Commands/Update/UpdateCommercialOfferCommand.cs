using MediatR;
using Request.Application.DTOs.CommercialOffer;

namespace Request.Application.Features.CommercialOffers.Commands.Update;

public sealed record UpdateCommercialOfferCommand(Guid Id, UpdateCommercialOfferDto Dto) : IRequest<CommercialOfferDto?>;

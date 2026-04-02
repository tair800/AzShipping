using MediatR;
using Request.Application.DTOs.CommercialOffer;

namespace Request.Application.Features.CommercialOffers.Commands.Create;

public sealed record CreateCommercialOfferCommand(CreateCommercialOfferDto Dto) : IRequest<CommercialOfferDto>;

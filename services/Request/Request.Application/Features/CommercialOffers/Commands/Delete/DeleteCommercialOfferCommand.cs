using MediatR;

namespace Request.Application.Features.CommercialOffers.Commands.Delete;

public sealed record DeleteCommercialOfferCommand(Guid Id) : IRequest<bool>;

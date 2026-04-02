using MediatR;
using Settings.Application.DTOs.PricingType;

namespace Settings.Application.Features.PricingTypes.Queries.GetById;

public sealed record GetPricingTypeByIdQuery(Guid Id) : IRequest<PricingTypeDto?>;

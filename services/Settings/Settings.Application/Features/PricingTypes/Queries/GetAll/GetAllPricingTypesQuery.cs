using MediatR;
using Settings.Application.DTOs.PricingType;

namespace Settings.Application.Features.PricingTypes.Queries.GetAll;

public sealed record GetAllPricingTypesQuery : IRequest<IReadOnlyList<PricingTypeDto>>;

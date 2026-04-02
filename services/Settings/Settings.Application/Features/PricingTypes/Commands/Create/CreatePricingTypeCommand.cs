using MediatR;
using Settings.Application.DTOs.PricingType;

namespace Settings.Application.Features.PricingTypes.Commands.Create;

public sealed record CreatePricingTypeCommand(CreatePricingTypeDto Dto) : IRequest<PricingTypeDto>;

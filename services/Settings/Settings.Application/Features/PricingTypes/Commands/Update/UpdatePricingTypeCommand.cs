using MediatR;
using Settings.Application.DTOs.PricingType;

namespace Settings.Application.Features.PricingTypes.Commands.Update;

public sealed record UpdatePricingTypeCommand(Guid Id, UpdatePricingTypeDto Dto) : IRequest<PricingTypeDto?>;

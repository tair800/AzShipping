using MediatR;

namespace Settings.Application.Features.PricingTypes.Commands.Delete;

public sealed record DeletePricingTypeCommand(Guid Id) : IRequest<bool>;

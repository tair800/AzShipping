using MediatR;

namespace Carrier.Application.Features.ShippingLines.Commands.Delete;

public record DeleteShippingLineCommand(Guid Id) : IRequest<bool>;

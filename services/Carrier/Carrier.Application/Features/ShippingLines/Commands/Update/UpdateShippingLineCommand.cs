using Carrier.Application.DTOs.ShippingLine;
using MediatR;

namespace Carrier.Application.Features.ShippingLines.Commands.Update;

public record UpdateShippingLineCommand(Guid Id, UpdateShippingLineDto Dto) : IRequest<ShippingLineDto?>;

using Carrier.Application.DTOs.ShippingLine;
using MediatR;

namespace Carrier.Application.Features.ShippingLines.Commands.Create;

public record CreateShippingLineCommand(CreateShippingLineDto Dto) : IRequest<ShippingLineDto>;

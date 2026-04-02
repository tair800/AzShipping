using Carrier.Application.DTOs.ShippingLine;
using MediatR;

namespace Carrier.Application.Features.ShippingLines.Queries.GetById;

public record GetShippingLineByIdQuery(Guid Id) : IRequest<ShippingLineDto?>;

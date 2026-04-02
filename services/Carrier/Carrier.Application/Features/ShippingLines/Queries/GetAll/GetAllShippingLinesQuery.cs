using Carrier.Application.DTOs.ShippingLine;
using MediatR;

namespace Carrier.Application.Features.ShippingLines.Queries.GetAll;

public record GetAllShippingLinesQuery(bool? IsActive) : IRequest<IReadOnlyList<ShippingLineDto>>;

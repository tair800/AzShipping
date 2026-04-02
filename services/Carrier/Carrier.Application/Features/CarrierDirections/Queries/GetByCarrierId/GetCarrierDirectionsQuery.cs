using Carrier.Application.DTOs.CarrierDirection;
using MediatR;

namespace Carrier.Application.Features.CarrierDirections.Queries.GetByCarrierId;

public record GetCarrierDirectionsQuery(Guid CarrierId) : IRequest<IReadOnlyList<CarrierDirectionDto>>;

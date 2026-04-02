using Carrier.Application.DTOs.Driver;
using MediatR;

namespace Carrier.Application.Features.Drivers.Queries.GetByCarrierId;

public record GetDriversByCarrierIdQuery(Guid CarrierId) : IRequest<IReadOnlyList<DriverDto>>;

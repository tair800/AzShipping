using Carrier.Application.DTOs.Driver;
using MediatR;

namespace Carrier.Application.Features.Drivers.Queries.GetById;

public record GetDriverByIdQuery(Guid Id) : IRequest<DriverDto?>;

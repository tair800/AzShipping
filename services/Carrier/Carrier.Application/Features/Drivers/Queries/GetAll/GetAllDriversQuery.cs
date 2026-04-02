using Carrier.Application.DTOs.Driver;
using MediatR;

namespace Carrier.Application.Features.Drivers.Queries.GetAll;

public record GetAllDriversQuery : IRequest<IReadOnlyList<DriverDto>>;

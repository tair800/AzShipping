using Carrier.Application.DTOs.Airline;
using MediatR;

namespace Carrier.Application.Features.Airlines.Queries.GetById;

public record GetAirlineByIdQuery(Guid Id) : IRequest<AirlineDto?>;

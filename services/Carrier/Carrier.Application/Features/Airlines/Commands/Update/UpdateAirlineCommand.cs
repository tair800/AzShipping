using Carrier.Application.DTOs.Airline;
using MediatR;

namespace Carrier.Application.Features.Airlines.Commands.Update;

public record UpdateAirlineCommand(Guid Id, UpdateAirlineDto Dto) : IRequest<AirlineDto?>;

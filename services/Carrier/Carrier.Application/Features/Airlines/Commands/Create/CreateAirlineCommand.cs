using Carrier.Application.DTOs.Airline;
using MediatR;

namespace Carrier.Application.Features.Airlines.Commands.Create;

public record CreateAirlineCommand(CreateAirlineDto Dto) : IRequest<AirlineDto>;

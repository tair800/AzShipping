using Carrier.Application.DTOs.Driver;
using MediatR;

namespace Carrier.Application.Features.Drivers.Commands.Create;

public record CreateDriverCommand(CreateDriverDto Dto) : IRequest<DriverDto>;

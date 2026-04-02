using Carrier.Application.DTOs.Driver;
using MediatR;

namespace Carrier.Application.Features.Drivers.Commands.Update;

public record UpdateDriverCommand(Guid Id, UpdateDriverDto Dto) : IRequest<DriverDto?>;

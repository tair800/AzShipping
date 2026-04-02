using Carrier.Application.DTOs.CarrierDirection;
using MediatR;

namespace Carrier.Application.Features.CarrierDirections.Commands.Update;

public record UpdateCarrierDirectionCommand(Guid Id, UpdateCarrierDirectionDto Dto) : IRequest<CarrierDirectionDto?>;

using Carrier.Application.DTOs.CarrierDirection;
using MediatR;

namespace Carrier.Application.Features.CarrierDirections.Commands.Create;

public record CreateCarrierDirectionCommand(Guid CarrierId, CreateCarrierDirectionDto Dto) : IRequest<CarrierDirectionDto>;

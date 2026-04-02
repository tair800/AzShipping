using MediatR;

namespace Carrier.Application.Features.CarrierDirections.Commands.Delete;

public record DeleteCarrierDirectionCommand(Guid Id) : IRequest<bool>;

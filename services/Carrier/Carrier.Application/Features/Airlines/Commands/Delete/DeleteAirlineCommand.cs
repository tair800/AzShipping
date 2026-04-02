using MediatR;

namespace Carrier.Application.Features.Airlines.Commands.Delete;

public record DeleteAirlineCommand(Guid Id) : IRequest<bool>;

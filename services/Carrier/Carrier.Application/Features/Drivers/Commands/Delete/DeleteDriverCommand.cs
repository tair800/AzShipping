using MediatR;

namespace Carrier.Application.Features.Drivers.Commands.Delete;

public record DeleteDriverCommand(Guid Id) : IRequest<bool>;

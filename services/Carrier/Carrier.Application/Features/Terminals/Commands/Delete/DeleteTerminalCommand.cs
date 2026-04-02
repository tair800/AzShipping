using MediatR;

namespace Carrier.Application.Features.Terminals.Commands.Delete;

public record DeleteTerminalCommand(Guid Id) : IRequest<bool>;

using Carrier.Application.DTOs.Terminal;
using MediatR;

namespace Carrier.Application.Features.Terminals.Commands.Update;

public record UpdateTerminalCommand(Guid Id, UpdateTerminalDto Dto) : IRequest<TerminalDto?>;

using Carrier.Application.DTOs.Terminal;
using MediatR;

namespace Carrier.Application.Features.Terminals.Commands.Create;

public record CreateTerminalCommand(CreateTerminalDto Dto) : IRequest<TerminalDto>;

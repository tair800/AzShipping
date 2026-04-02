using Carrier.Application.DTOs.Terminal;
using MediatR;

namespace Carrier.Application.Features.Terminals.Queries.GetById;

public record GetTerminalByIdQuery(Guid Id) : IRequest<TerminalDto?>;

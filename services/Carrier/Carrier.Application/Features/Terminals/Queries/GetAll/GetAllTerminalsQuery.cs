using Carrier.Application.DTOs.Terminal;
using MediatR;

namespace Carrier.Application.Features.Terminals.Queries.GetAll;

public record GetAllTerminalsQuery : IRequest<IReadOnlyList<TerminalDto>>;

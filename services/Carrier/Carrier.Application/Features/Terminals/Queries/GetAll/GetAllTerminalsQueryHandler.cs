using Carrier.Application.DTOs.Terminal;
using Carrier.Application.Features.Terminals;
using Carrier.Domain.AggregatesModel.TerminalAggregate;
using MediatR;

namespace Carrier.Application.Features.Terminals.Queries.GetAll;

public sealed class GetAllTerminalsQueryHandler(ITerminalRepository repository) : IRequestHandler<GetAllTerminalsQuery, IReadOnlyList<TerminalDto>>
{
    public async Task<IReadOnlyList<TerminalDto>> Handle(GetAllTerminalsQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(cancellationToken);
        return items.Select(TerminalMapper.MapToDto).ToList();
    }
}

using Carrier.Application.DTOs.Terminal;
using Carrier.Application.Features.Terminals;
using Carrier.Domain.AggregatesModel.TerminalAggregate;
using MediatR;

namespace Carrier.Application.Features.Terminals.Queries.GetById;

public sealed class GetTerminalByIdQueryHandler(ITerminalRepository repository) : IRequestHandler<GetTerminalByIdQuery, TerminalDto?>
{
    public async Task<TerminalDto?> Handle(GetTerminalByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : TerminalMapper.MapToDto(entity);
    }
}

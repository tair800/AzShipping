using MediatR;
using Request.Application.DTOs.PriceProposal;
using Request.Application.Features.PriceProposals;
using Request.Domain.AggregatesModel.PriceProposalAggregate;

namespace Request.Application.Features.PriceProposals.Queries.GetById;

public sealed class GetPriceProposalByIdQueryHandler(IPriceProposalRepository repository)
    : IRequestHandler<GetPriceProposalByIdQuery, PriceProposalDto?>
{
    public async Task<PriceProposalDto?> Handle(GetPriceProposalByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : PriceProposalMapper.MapToDto(entity);
    }
}

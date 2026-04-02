using MediatR;
using Request.Application.DTOs.PriceProposal;
using Request.Application.Features.PriceProposals;
using Request.Domain.AggregatesModel.PriceProposalAggregate;

namespace Request.Application.Features.PriceProposals.Queries.GetByRequestId;

public sealed class GetPriceProposalsByRequestIdQueryHandler(IPriceProposalRepository repository)
    : IRequestHandler<GetPriceProposalsByRequestIdQuery, IReadOnlyList<PriceProposalDto>>
{
    public async Task<IReadOnlyList<PriceProposalDto>> Handle(GetPriceProposalsByRequestIdQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetByRequestIdAsync(request.RequestId, cancellationToken);
        return list.Select(PriceProposalMapper.MapToDto).ToList();
    }
}

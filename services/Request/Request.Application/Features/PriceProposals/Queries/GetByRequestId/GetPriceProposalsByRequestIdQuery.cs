using MediatR;
using Request.Application.DTOs.PriceProposal;

namespace Request.Application.Features.PriceProposals.Queries.GetByRequestId;

public sealed record GetPriceProposalsByRequestIdQuery(Guid RequestId) : IRequest<IReadOnlyList<PriceProposalDto>>;

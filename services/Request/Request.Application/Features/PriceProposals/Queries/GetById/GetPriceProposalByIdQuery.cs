using MediatR;
using Request.Application.DTOs.PriceProposal;

namespace Request.Application.Features.PriceProposals.Queries.GetById;

public sealed record GetPriceProposalByIdQuery(Guid Id) : IRequest<PriceProposalDto?>;

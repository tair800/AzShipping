using MediatR;
using Request.Application.DTOs.PriceProposal;

namespace Request.Application.Features.PriceProposals.Commands.Create;

public sealed record CreatePriceProposalCommand(CreatePriceProposalDto Dto) : IRequest<PriceProposalDto>;

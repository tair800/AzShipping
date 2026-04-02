using MediatR;
using Request.Application.DTOs.PriceProposal;

namespace Request.Application.Features.PriceProposals.Commands.Update;

public sealed record UpdatePriceProposalCommand(Guid Id, UpdatePriceProposalDto Dto) : IRequest<PriceProposalDto?>;

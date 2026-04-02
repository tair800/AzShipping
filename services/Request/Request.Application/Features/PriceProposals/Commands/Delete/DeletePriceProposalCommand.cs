using MediatR;

namespace Request.Application.Features.PriceProposals.Commands.Delete;

public sealed record DeletePriceProposalCommand(Guid Id) : IRequest<bool>;

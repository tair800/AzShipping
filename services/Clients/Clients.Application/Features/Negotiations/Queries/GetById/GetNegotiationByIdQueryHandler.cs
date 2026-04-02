using Clients.Application.DTOs.Negotiation;
using Clients.Domain.AggregatesModel.NegotiationAggregate;
using MediatR;

namespace Clients.Application.Features.Negotiations.Queries.GetById;

public sealed class GetNegotiationByIdQueryHandler(
    INegotiationRepository repository,
    INegotiationResultRepository resultRepository) : IRequestHandler<GetNegotiationByIdQuery, NegotiationDto?>
{
    public async Task<NegotiationDto?> Handle(GetNegotiationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        var results = await resultRepository.GetByNegotiationIdAsync(entity.Id, cancellationToken);
        return new NegotiationDto
        {
            Id = entity.Id,
            ClientId = entity.ClientId,
            PersonName = entity.PersonName,
            CreationDate = entity.CreationDate,
            WayOfNegotiationId = entity.WayOfNegotiationId,
            QuestionsAndAnswers = entity.QuestionsAndAnswers,
            Result = entity.Result,
            Results = results.Select(r => new NegotiationResultDto(r.Id, r.NegotiationId, r.Result, r.Comments, r.ResultDate)).ToList()
        };
    }
}

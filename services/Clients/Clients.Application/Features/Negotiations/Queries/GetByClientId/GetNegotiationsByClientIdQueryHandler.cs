using Clients.Application.DTOs.Negotiation;
using Clients.Domain.AggregatesModel.NegotiationAggregate;
using MediatR;

namespace Clients.Application.Features.Negotiations.Queries.GetByClientId;

public sealed class GetNegotiationsByClientIdQueryHandler(
    INegotiationRepository repository,
    INegotiationResultRepository resultRepository) : IRequestHandler<GetNegotiationsByClientIdQuery, IReadOnlyList<NegotiationDto>>
{
    public async Task<IReadOnlyList<NegotiationDto>> Handle(GetNegotiationsByClientIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetByClientIdAsync(request.ClientId, cancellationToken);
        var result = new List<NegotiationDto>();
        foreach (var e in entities)
        {
            var results = await resultRepository.GetByNegotiationIdAsync(e.Id, cancellationToken);
            result.Add(new NegotiationDto
            {
                Id = e.Id,
                ClientId = e.ClientId,
                PersonName = e.PersonName,
                CreationDate = e.CreationDate,
                WayOfNegotiationId = e.WayOfNegotiationId,
                QuestionsAndAnswers = e.QuestionsAndAnswers,
                Result = e.Result,
                Results = results.Select(r => new NegotiationResultDto(r.Id, r.NegotiationId, r.Result, r.Comments, r.ResultDate)).ToList()
            });
        }
        return result;
    }
}

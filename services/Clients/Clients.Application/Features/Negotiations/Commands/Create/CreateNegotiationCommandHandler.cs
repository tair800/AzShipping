using Clients.Application.DTOs.Negotiation;
using Clients.Application.Services;
using Clients.Domain.AggregatesModel.NegotiationAggregate;
using MediatR;

namespace Clients.Application.Features.Negotiations.Commands.Create;

public sealed class CreateNegotiationCommandHandler(
    INegotiationRepository repository,
    INegotiationResultRepository resultRepository,
    IActionLogClient actionLogClient) : IRequestHandler<CreateNegotiationCommand, NegotiationDto>
{
    public async Task<NegotiationDto> Handle(CreateNegotiationCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var creationDate = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(dto.CreationDate) && DateTime.TryParse(dto.CreationDate, null, System.Globalization.DateTimeStyles.RoundtripKind, out var parsed))
            creationDate = parsed.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(parsed, DateTimeKind.Utc) : parsed.ToUniversalTime();
        var entity = new Negotiation
        {
            Id = Guid.NewGuid(),
            ClientId = dto.ClientId,
            PersonName = dto.PersonName,
            CreationDate = creationDate,
            WayOfNegotiationId = dto.WayOfNegotiationId,
            QuestionsAndAnswers = dto.QuestionsAndAnswers,
            Result = dto.Result
        };
        await repository.AddAsync(entity, cancellationToken);

        if (dto.Results is { Count: > 0 })
        {
            foreach (var r in dto.Results)
            {
                var resultDate = DateTime.UtcNow;
                if (!string.IsNullOrWhiteSpace(r.ResultDate) && DateTime.TryParse(r.ResultDate, null, System.Globalization.DateTimeStyles.RoundtripKind, out var pr))
                    resultDate = pr.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(pr, DateTimeKind.Utc) : pr.ToUniversalTime();
                await resultRepository.AddAsync(new NegotiationResult
                {
                    Id = Guid.NewGuid(),
                    NegotiationId = entity.Id,
                    Result = r.Result ?? string.Empty,
                    Comments = r.Comments,
                    ResultDate = resultDate
                }, cancellationToken);
            }
        }

        var results = await resultRepository.GetByNegotiationIdAsync(entity.Id, cancellationToken);
        var dtoResult = MapToDto(entity, results);
        await actionLogClient.LogAsync("Client negotiation created", $"negotiation: client {entity.ClientId} • person: {entity.PersonName} • id: {entity.Id}", null, null, cancellationToken);
        return dtoResult;
    }

    private static NegotiationDto MapToDto(Negotiation e, IReadOnlyList<NegotiationResult> results) => new()
    {
        Id = e.Id,
        ClientId = e.ClientId,
        PersonName = e.PersonName,
        CreationDate = e.CreationDate,
        WayOfNegotiationId = e.WayOfNegotiationId,
        QuestionsAndAnswers = e.QuestionsAndAnswers,
        Result = e.Result,
        Results = results.Select(r => new NegotiationResultDto(r.Id, r.NegotiationId, r.Result, r.Comments, r.ResultDate)).ToList()
    };
}

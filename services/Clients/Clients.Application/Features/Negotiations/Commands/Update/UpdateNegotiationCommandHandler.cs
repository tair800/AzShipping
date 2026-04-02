using Clients.Application.DTOs.Negotiation;
using Clients.Application.Services;
using Clients.Domain.AggregatesModel.NegotiationAggregate;
using MediatR;

namespace Clients.Application.Features.Negotiations.Commands.Update;

public sealed class UpdateNegotiationCommandHandler(
    INegotiationRepository repository,
    INegotiationResultRepository resultRepository,
    IActionLogClient actionLogClient) : IRequestHandler<UpdateNegotiationCommand, NegotiationDto?>
{
    public async Task<NegotiationDto?> Handle(UpdateNegotiationCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;
        entity.PersonName = dto.PersonName;
        if (!string.IsNullOrWhiteSpace(dto.CreationDate) && DateTime.TryParse(dto.CreationDate, null, System.Globalization.DateTimeStyles.RoundtripKind, out var parsed))
            entity.CreationDate = parsed.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(parsed, DateTimeKind.Utc) : parsed.ToUniversalTime();
        entity.WayOfNegotiationId = dto.WayOfNegotiationId;
        entity.QuestionsAndAnswers = dto.QuestionsAndAnswers;
        entity.Result = dto.Result;

        await repository.UpdateAsync(entity, cancellationToken);

        await resultRepository.DeleteByNegotiationIdAsync(entity.Id, cancellationToken);
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
        await actionLogClient.LogAsync("Client negotiation updated", $"negotiation: client {entity.ClientId} • person: {entity.PersonName} • id: {entity.Id}", null, null, cancellationToken);
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

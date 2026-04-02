using MediatR;
using Settings.Application.DTOs.Numeration;
using Settings.Application.Features.Numerations;
using Settings.Domain.AggregatesModel.NumerationAggregate;

namespace Settings.Application.Features.Numerations.Queries.Preview;

public sealed class PreviewNumerationQueryHandler(INumerationRepository repository)
    : IRequestHandler<PreviewNumerationQuery, NumerationGenerateResponseDto>
{
    public async Task<NumerationGenerateResponseDto> Handle(PreviewNumerationQuery request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        if (string.IsNullOrWhiteSpace(dto.NumerationForCode))
            throw new InvalidOperationException("numerationForCode is required.");

        var candidates = await repository.GetCandidatesAsync(dto.NumerationForCode, cancellationToken);
        var resolved = NumerationGenerationEngine.ResolveRule(candidates, dto)
            ?? throw new InvalidOperationException("No numeration rule found for supplied dimensions.");

        var simulatedIndex = resolved.Rule.CurrentIndex + 1;
        var value = NumerationGenerationEngine.Render(resolved.Rule, dto, simulatedIndex);
        return new NumerationGenerateResponseDto(
            resolved.Rule.Id,
            resolved.Rule.Name,
            value,
            simulatedIndex,
            resolved.Score,
            resolved.Rule.IsSystemic || resolved.Score == 0,
            resolved.Rule.Formula);
    }
}

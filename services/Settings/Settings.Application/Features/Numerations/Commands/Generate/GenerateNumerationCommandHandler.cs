using MediatR;
using Settings.Application.DTOs.Numeration;
using Settings.Application.Features.Numerations;
using Settings.Domain.AggregatesModel.NumerationAggregate;

namespace Settings.Application.Features.Numerations.Commands.Generate;

public sealed class GenerateNumerationCommandHandler(INumerationRepository repository)
    : IRequestHandler<GenerateNumerationCommand, NumerationGenerateResponseDto>
{
    public async Task<NumerationGenerateResponseDto> Handle(GenerateNumerationCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        if (string.IsNullOrWhiteSpace(dto.NumerationForCode))
            throw new InvalidOperationException("numerationForCode is required.");

        var candidates = await repository.GetCandidatesAsync(dto.NumerationForCode, cancellationToken);
        var resolved = NumerationGenerationEngine.ResolveRule(candidates, dto)
            ?? throw new InvalidOperationException("No numeration rule found for supplied dimensions.");

        var nextIndex = await repository.IncrementIndexAtomicallyAsync(resolved.Rule.Id, cancellationToken)
            ?? throw new InvalidOperationException("Resolved numeration rule not found for atomic increment.");

        var value = NumerationGenerationEngine.Render(resolved.Rule, dto, nextIndex);
        return new NumerationGenerateResponseDto(
            resolved.Rule.Id,
            resolved.Rule.Name,
            value,
            nextIndex,
            resolved.Score,
            resolved.Rule.IsSystemic || resolved.Score == 0,
            resolved.Rule.Formula);
    }
}

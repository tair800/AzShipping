using Accounting.Application.DTOs.VatDefinition;
using Accounting.Domain;
using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Commands.Calculate;

public sealed class CalculateVatFromNetCommandHandler(IVatDefinitionRepository repository)
    : IRequestHandler<CalculateVatFromNetCommand, CalculateVatFromNetResultDto?>
{
    public async Task<CalculateVatFromNetResultDto?> Handle(CalculateVatFromNetCommand command, CancellationToken cancellationToken)
    {
        var req = command.Request;
        var def = await repository.GetByIdAsync(req.VatDefinitionId, cancellationToken);
        if (def == null || !def.IsActive) return null;
        var (_, vat, gross) = VatCalculation.SplitFromNet(req.AmountExcludingVat, def.Percent);
        return new CalculateVatFromNetResultDto(req.AmountExcludingVat, def.Percent, vat, gross);
    }
}

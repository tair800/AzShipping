using Accounting.Application.DTOs.VatDefinition;
using Accounting.Application.Features.VatDefinitions;
using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Queries.Legacy;

public sealed class GetAllVatRatesLegacyQueryHandler(IVatDefinitionRepository repository)
    : IRequestHandler<GetAllVatRatesLegacyQuery, IReadOnlyList<VatRateLegacyDto>>
{
    public async Task<IReadOnlyList<VatRateLegacyDto>> Handle(GetAllVatRatesLegacyQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(VatDefinitionMapper.ToLegacy).ToList();
    }
}

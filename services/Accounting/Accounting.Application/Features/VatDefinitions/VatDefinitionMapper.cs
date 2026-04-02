using Accounting.Application.DTOs.VatDefinition;
using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;

namespace Accounting.Application.Features.VatDefinitions;

public static class VatDefinitionMapper
{
    public static VatDefinitionDto ToDto(VatDefinition e) => new(
        e.Id,
        e.CreatedAtUtc,
        e.UpdatedAtUtc,
        e.Name,
        e.Percent,
        e.IsAlcohol,
        e.BuyingAccountName,
        e.BuyingAccountCode,
        e.SellingAccountName,
        e.SellingAccountCode,
        e.Notes,
        e.IsActive);

    public static VatRateLegacyDto ToLegacy(VatDefinition e) => new(
        e.Id,
        e.Name,
        e.Percent,
        e.IsActive,
        e.CreatedAtUtc,
        e.UpdatedAtUtc);
}

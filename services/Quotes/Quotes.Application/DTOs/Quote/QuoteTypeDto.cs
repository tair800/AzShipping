namespace Quotes.Application.DTOs.Quote;

public record QuoteTypeDto(
    Guid Id,
    string Code,
    string Name,
    string Direction,
    string Mode,
    string? SubType,
    string QuoteNumberPrefix,
    string CarrierApiPath,
    string CarrierLabel,
    int SortOrder,
    bool IsActive,
    string FillDimensionsTitle,
    string FillDimensionsVolumetricWeightLabel,
    string FillDimensionsVolumetricWeightTooltip,
    string FillDimensionsChargeableWeightLabel);

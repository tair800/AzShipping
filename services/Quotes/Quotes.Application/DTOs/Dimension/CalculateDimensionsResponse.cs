namespace Quotes.Application.DTOs.Dimension;

/// <summary>
/// Response for dimension calculation. For Sea/Road/Rail: TotalVolumetricWeightKg and ChargeableWeightKg hold MT values.
/// </summary>
public record CalculateDimensionsResponse(
    decimal TotalGrossWeightKg,
    decimal TotalVolumeCbm,
    decimal TotalVolumetricWeightKg,
    decimal ChargeableWeightKg,
    int NumberOfPackages,
    IReadOnlyList<DimensionRowResult>? Rows = null);

public record DimensionRowResult(decimal VolumeCbm, decimal VolumetricWeightKg);

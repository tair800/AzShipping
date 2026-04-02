namespace Request.API.Models;

public record CalculateDimensionsResponse(
    decimal TotalGrossWeightKg,
    decimal TotalVolumeCbm,
    decimal TotalVolumetricWeightKg,
    decimal ChargeableWeightKg,
    int NumberOfPackages,
    IReadOnlyList<DimensionRowResult>? Rows = null);

public record DimensionRowResult(decimal VolumeCbm, decimal VolumetricWeightKg);

// For Sea mode: TotalVolumetricWeightKg and ChargeableWeightKg hold MT values.

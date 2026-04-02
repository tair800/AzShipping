namespace Quotes.Application.DTOs.Dimension;

public record CalculateDimensionsRequest(
    IReadOnlyList<DimensionInputDto>? Dimensions = null,
    decimal? VolumeCbm = null,
    decimal? GrossWeightKg = null,
    string? Mode = null);

public record DimensionInputDto(decimal Length, decimal Width, decimal Height, int Quantity, decimal? WeightKg);

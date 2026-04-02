namespace Request.Application.Services;

/// <summary>
/// Business rules for Export Air Request dimension calculations.
/// Avia: 1 CBM = 166.67 kg. Volume = (L×W×H) × Quantity. Dimensions in cm, auto-converted to CBM.
/// Chargeable weight = rounded volumetric weight.
/// </summary>
public static class ExportAirRequestCalculationService
{
    public const decimal VolumetricFactorKgPerCbm = 166.67m;

    /// <summary>
    /// Volume in CBM from dimensions in cm. Formula: (L×W×H) × Quantity, cm³ converted to m³.
    /// </summary>
    public static decimal CalculateVolumeCbm(decimal lengthCm, decimal widthCm, decimal heightCm, int quantity)
    {
        return (lengthCm * widthCm * heightCm * quantity) / 1_000_000m;
    }

    /// <summary>
    /// Volumetric weight = Volume × 166.67 (1 CBM = 166.67 kg for Avia).
    /// </summary>
    public static decimal CalculateVolumetricWeight(decimal volumeCbm)
    {
        return volumeCbm * VolumetricFactorKgPerCbm;
    }

    /// <summary>
    /// Chargeable weight = rounded volumetric weight.
    /// </summary>
    public static decimal RoundChargeableWeight(decimal volumetricWeightKg)
    {
        return Math.Round(volumetricWeightKg);
    }

    public record DimensionTotals(
        decimal TotalGrossWeightKg,
        decimal TotalVolumeCbm,
        decimal TotalVolumetricWeightKg,
        decimal ChargeableWeightKg,
        int NumberOfPackages,
        IReadOnlyList<DimensionRowResult> Rows);

    public record DimensionRowResult(decimal VolumeCbm, decimal VolumetricWeightKg);

    /// <summary>
    /// Computes totals from dimension rows. Frontend sends Length, Width, Height, Quantity, WeightKg (gross) per row.
    /// </summary>
    public static DimensionTotals CalculateTotals(IReadOnlyList<DimensionInput> rows)
    {
        if (rows == null || rows.Count == 0)
            return new DimensionTotals(0, 0, 0, 0, 0, []);

        decimal totalGross = 0;
        decimal totalVolume = 0;
        decimal totalVolumetric = 0;
        int totalQty = 0;
        var rowResults = new List<DimensionRowResult>();

        foreach (var r in rows)
        {
            var qty = Math.Max(1, r.Quantity);
            var vol = CalculateVolumeCbm(r.Length, r.Width, r.Height, qty);
            var volWeight = CalculateVolumetricWeight(vol);

            totalGross += r.WeightKg ?? 0;
            totalVolume += vol;
            totalVolumetric += volWeight;
            totalQty += qty;
            rowResults.Add(new DimensionRowResult(vol, volWeight));
        }

        var chargeable = RoundChargeableWeight(totalVolumetric);

        return new DimensionTotals(totalGross, totalVolume, totalVolumetric, chargeable, totalQty, rowResults);
    }

    /// <summary>
    /// When no dimensions: Chargeable = rounded(Volume × 166.67). Used for manual entry.
    /// </summary>
    public static decimal? CalculateChargeableFromManualVolume(decimal? volumeCbm)
    {
        if (volumeCbm is null or <= 0) return null;
        return RoundChargeableWeight((decimal)volumeCbm * VolumetricFactorKgPerCbm);
    }

    public record DimensionInput(decimal Length, decimal Width, decimal Height, int Quantity, decimal? WeightKg);
}

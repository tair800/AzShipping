namespace Request.Application.Services;

/// <summary>
/// Business rules for Sea freight dimension calculations.
/// Ocean: 1 CBM = 1 MT. Volume = (L×W×H) × Quantity. Dimensions in cm, auto-converted to CBM.
/// Volumetric Weight (MT) = Volume (CBM). Chargeable weight = rounded volumetric weight (MT).
/// </summary>
public static class SeaFreightCalculationService
{
    /// <summary>
    /// Volume in CBM from dimensions in cm. Formula: (L×W×H) × Quantity, cm³ converted to m³.
    /// </summary>
    public static decimal CalculateVolumeCbm(decimal lengthCm, decimal widthCm, decimal heightCm, int quantity)
    {
        return (lengthCm * widthCm * heightCm * quantity) / 1_000_000m;
    }

    /// <summary>
    /// Ocean: 1 CBM = 1 MT. Volumetric weight (MT) = Volume (CBM).
    /// </summary>
    public static decimal CalculateVolumetricWeightMt(decimal volumeCbm)
    {
        return volumeCbm;
    }

    /// <summary>
    /// Chargeable weight = rounded volumetric weight (MT). Rounded to 3 decimals for sea.
    /// </summary>
    public static decimal RoundChargeableWeight(decimal volumetricWeightMt)
    {
        return Math.Round(volumetricWeightMt, 3);
    }

    public record DimensionTotals(
        decimal TotalGrossWeightKg,
        decimal TotalVolumeCbm,
        decimal TotalVolumetricWeightMt,
        decimal ChargeableWeightMt,
        int NumberOfPackages,
        IReadOnlyList<DimensionRowResult> Rows);

    public record DimensionRowResult(decimal VolumeCbm, decimal VolumetricWeightMt);

    /// <summary>
    /// Computes totals from dimension rows for sea freight.
    /// </summary>
    public static DimensionTotals CalculateTotals(IReadOnlyList<ExportAirRequestCalculationService.DimensionInput> rows)
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
            var volWeight = CalculateVolumetricWeightMt(vol);

            totalGross += r.WeightKg ?? 0;
            totalVolume += vol;
            totalVolumetric += volWeight;
            totalQty += qty;
            rowResults.Add(new DimensionRowResult(vol, volWeight));
        }

        var chargeable = RoundChargeableWeight(totalVolumetric);
        return new DimensionTotals(totalGross, totalVolume, totalVolumetric, chargeable, totalQty, rowResults);
    }
}

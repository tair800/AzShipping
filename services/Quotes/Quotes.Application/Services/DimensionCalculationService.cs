namespace Quotes.Application.Services;

/// <summary>
/// Dimension calculations for quotes. Same rules as Requests:
/// - Sea, Road, Rail: 1 CBM = 1 MT. Chargeable = max(Gross, Volumetric) rounded to 3 decimals.
/// - Air: 1 CBM = 166.67 kg. Chargeable = max(Gross, Volumetric) rounded to kg.
/// </summary>
public static class DimensionCalculationService
{
    public const decimal AirVolumetricFactorKgPerCbm = 166.67m;

    public record DimensionInput(decimal Length, decimal Width, decimal Height, int Quantity, decimal? WeightKg);

    public record CalculateResult(
        decimal TotalGrossWeightKg,
        decimal TotalVolumeCbm,
        decimal TotalVolumetricWeightKg,
        decimal ChargeableWeightKg,
        int NumberOfPackages,
        IReadOnlyList<RowResult>? Rows);

    public record RowResult(decimal VolumeCbm, decimal VolumetricWeightKg);

    /// <summary>
    /// Volume in CBM from dimensions in cm. Formula: (L×W×H) × Quantity, cm³ converted to m³.
    /// </summary>
    public static decimal CalculateVolumeCbm(decimal lengthCm, decimal widthCm, decimal heightCm, int quantity)
    {
        return (lengthCm * widthCm * heightCm * quantity) / 1_000_000m;
    }

    /// <summary>
    /// Sea/Road/Rail: 1 CBM = 1 MT. Volumetric = Volume (CBM). Response TotalVolumetricWeightKg holds MT.
    /// </summary>
    public static decimal CalculateVolumetricWeightSeaRoadRail(decimal volumeCbm)
    {
        return volumeCbm;
    }

    /// <summary>
    /// Air: Volumetric weight = Volume × 166.67 kg.
    /// </summary>
    public static decimal CalculateVolumetricWeightAir(decimal volumeCbm)
    {
        return volumeCbm * AirVolumetricFactorKgPerCbm;
    }

    /// <summary>
    /// Chargeable = rounded volumetric. Sea/Road/Rail: round to 3 decimals (MT). Air: round to kg.
    /// </summary>
    public static decimal RoundChargeable(bool useSeaFormula, decimal totalVolumetric)
    {
        return useSeaFormula ? Math.Round(totalVolumetric, 3) : Math.Round(totalVolumetric);
    }

    /// <summary>
    /// Computes totals from dimension rows. Mode: Sea, Road, Rail, or Air.
    /// </summary>
    public static CalculateResult Calculate(IReadOnlyList<DimensionInput> rows, string mode)
    {
        var useSeaFormula = string.Equals(mode, "Sea", StringComparison.OrdinalIgnoreCase)
            || string.Equals(mode, "Road", StringComparison.OrdinalIgnoreCase)
            || string.Equals(mode, "Rail", StringComparison.OrdinalIgnoreCase);

        if (rows == null || rows.Count == 0)
            return new CalculateResult(0, 0, 0, 0, 0, null);

        decimal totalGross = 0;
        decimal totalVolume = 0;
        decimal totalVolumetric = 0;
        int totalQty = 0;
        var rowResults = new List<RowResult>();

        foreach (var r in rows)
        {
            var qty = Math.Max(1, r.Quantity);
            var vol = CalculateVolumeCbm(r.Length, r.Width, r.Height, qty);
            var volWeight = useSeaFormula ? CalculateVolumetricWeightSeaRoadRail(vol) : CalculateVolumetricWeightAir(vol);

            totalGross += r.WeightKg ?? 0;
            totalVolume += vol;
            totalVolumetric += volWeight;
            totalQty += qty;
            rowResults.Add(new RowResult(vol, volWeight));
        }

        var chargeable = RoundChargeable(useSeaFormula, totalVolumetric);
        return new CalculateResult(totalGross, totalVolume, totalVolumetric, chargeable, totalQty, rowResults);
    }

    /// <summary>
    /// When user enters Volume manually (no dimensions). Chargeable from volume only.
    /// </summary>
    public static CalculateResult CalculateFromManualVolume(decimal volumeCbm, decimal? grossWeightKg, string mode)
    {
        if (volumeCbm <= 0)
            return new CalculateResult(grossWeightKg ?? 0, 0, 0, 0, 0, null);

        var useSeaFormula = string.Equals(mode, "Sea", StringComparison.OrdinalIgnoreCase)
            || string.Equals(mode, "Road", StringComparison.OrdinalIgnoreCase)
            || string.Equals(mode, "Rail", StringComparison.OrdinalIgnoreCase);

        var volWeight = useSeaFormula ? volumeCbm : volumeCbm * AirVolumetricFactorKgPerCbm;
        var chargeable = RoundChargeable(useSeaFormula, volWeight);
        var gross = grossWeightKg ?? 0;

        return new CalculateResult(gross, volumeCbm, volWeight, chargeable, 0, null);
    }
}

namespace Operation.Application.Services;

/// <summary>Dimensions in cm. Chargeable per line = max(line gross kg, line volumetric kg); total chargeable = sum of line chargeables. Air: 166.67 kg/CBM; sea LCL (W/M): 1000 kg/CBM.</summary>
public static class AirFreightCalculationService
{
    public const decimal VolumetricFactorKgPerCbm = 166.67m;

    /// <summary>Sea LCL consolidation: 1 CBM = 1 metric ton freight tons (common W/M basis).</summary>
    public const decimal SeaLclVolumetricFactorKgPerCbm = 1000m;

    public static decimal CalculateVolumeCbm(decimal lengthCm, decimal widthCm, decimal heightCm, int quantity)
        => (lengthCm * widthCm * heightCm * quantity) / 1_000_000m;

    public static decimal CalculateVolumetricWeight(decimal volumeCbm) => CalculateVolumetricWeight(volumeCbm, VolumetricFactorKgPerCbm);

    public static decimal CalculateVolumetricWeight(decimal volumeCbm, decimal volumetricKgPerCbm) => volumeCbm * volumetricKgPerCbm;

    /// <summary>Uses <paramref name="manualVolumeCbm"/> when &gt; 0; otherwise L×W×H×qty in cm³.</summary>
    public static decimal GetLineVolumeCbm(decimal lengthCm, decimal widthCm, decimal heightCm, int quantity, decimal? manualVolumeCbm)
    {
        if (manualVolumeCbm is > 0m) return manualVolumeCbm.Value;
        if (lengthCm > 0 && widthCm > 0 && heightCm > 0)
            return CalculateVolumeCbm(lengthCm, widthCm, heightCm, Math.Max(1, quantity));
        return 0m;
    }

    public static (decimal TotalGrossWeightKg, decimal TotalVolumeCbm, decimal TotalVolumetricWeightKg, decimal ChargeableWeightKg, int NumberOfPackages) CalculateTotals(
        IReadOnlyList<DimensionInput> rows)
        => CalculateTotals(rows, VolumetricFactorKgPerCbm);

    public static (decimal TotalGrossWeightKg, decimal TotalVolumeCbm, decimal TotalVolumetricWeightKg, decimal ChargeableWeightKg, int NumberOfPackages) CalculateTotals(
        IReadOnlyList<DimensionInput> rows, decimal volumetricKgPerCbm)
    {
        if (rows == null || rows.Count == 0)
            return (0, 0, 0, 0, 0);

        decimal totalGross = 0;
        decimal totalVolume = 0;
        decimal totalVolumetric = 0;
        decimal totalChargeable = 0;
        var totalQty = 0;

        foreach (var r in rows)
        {
            var qty = Math.Max(1, r.Quantity);
            var vol = GetLineVolumeCbm(r.Length, r.Width, r.Height, qty, r.VolumeCbm);
            var lineVolW = CalculateVolumetricWeight(vol, volumetricKgPerCbm);
            var gw = r.WeightKg ?? 0m;
            totalGross += gw;
            totalVolume += vol;
            totalVolumetric += lineVolW;
            totalChargeable += Math.Max(gw, lineVolW);
            totalQty += qty;
        }

        var roundedChargeable = Math.Round(totalChargeable, 3, MidpointRounding.AwayFromZero);
        return (totalGross, totalVolume, totalVolumetric, roundedChargeable, totalQty);
    }

    public static decimal? CalculateChargeableFromManualVolume(decimal? volumeCbm)
        => CalculateChargeableFromManualVolume(volumeCbm, VolumetricFactorKgPerCbm);

    public static decimal? CalculateChargeableFromManualVolume(decimal? volumeCbm, decimal volumetricKgPerCbm)
    {
        if (volumeCbm is null or <= 0) return null;
        return Math.Round(volumeCbm.Value * volumetricKgPerCbm, 3, MidpointRounding.AwayFromZero);
    }

    public record DimensionInput(decimal Length, decimal Width, decimal Height, int Quantity, decimal? WeightKg, decimal? VolumeCbm = null);
}

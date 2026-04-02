using Request.Application.DTOs.Request;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Features.Requests;

public static class RequestMapper
{
    public static RequestDto MapToDto(RequestEntity entity, RequestType? requestType, IReadOnlyList<RequestDimension> dimensions, IReadOnlyList<RequestVas>? vasItems = null)
    {
        var dims = dimensions ?? [];
        var vas = vasItems ?? [];
        return new RequestDto(
            entity.Id,
            entity.CreationDate,
            entity.RequestNumber,
            entity.RequestTypeId,
            requestType?.Code,
            requestType?.Name,
            entity.CompanyId,
            entity.CompanyName,
            entity.ManagerId,
            entity.ManagerName,
            entity.LogisticianId,
            entity.LogisticianName,
            entity.DepartmentId,
            entity.DepartmentName,
            entity.ShipperId,
            entity.ShipperName,
            entity.ConsigneeId,
            entity.ConsigneeName,
            entity.MyCustomerTypeId,
            entity.MyCustomerTypeName,
            entity.DispatchDateFrom,
            entity.DispatchDateTo,
            entity.UnloadingDateFrom,
            entity.UnloadingDateTo,
            entity.QuotationSent,
            entity.StatusName,
            entity.ExtremelyUrgent,
            entity.ToAnswerUntilDate,
            entity.PriceStandard,
            entity.CurrencyId,
            entity.CurrencyCode,
            entity.PriceWithVat,
            entity.VatRate,
            entity.SourceOfRequestId,
            entity.SourceOfRequestName,
            entity.RequestPurposeId,
            entity.RequestPurposeName,
            entity.GatewayTerminalId,
            entity.GatewayName,
            entity.ViaPortTerminalId,
            entity.ViaPortName,
            entity.DestinationTerminalId,
            entity.DestinationName,
            entity.ViaPort2TerminalId,
            entity.ViaPort2Name,
            entity.TransitPortTerminalId,
            entity.TransitPortName,
            entity.CarrierId,
            entity.CarrierName,
            entity.StationOfDeliveryTerminalId,
            entity.StationOfDeliveryName,
            entity.GrossWeightKg,
            entity.VolumeCbm,
            entity.ChargeableWeightKg,
            entity.NumberOfPackages,
            entity.DangerousGoods,
            entity.DescriptionOfGoods,
            entity.Notes,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt,
            dims.Select(d => new RequestDimensionDto(d.Id, d.Length, d.Width, d.Height, d.Quantity, d.WeightKg, d.VolumeCbm, d.PackageType)).ToList(),
            vas.Select(v => new RequestVasDto(v.Id, v.VasId, v.VasName, v.ExecutionPlace, v.Quantity, v.Uom, v.CurrencyId, v.CurrencyCode, v.Total, v.Notes)).ToList());
    }

    public static RequestTypeDto MapTypeToDto(RequestType t)
    {
        var supportsVas = (string.Equals(t.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase)
                && (string.Equals(t.Mode, "Sea", StringComparison.OrdinalIgnoreCase) || string.Equals(t.Mode, "Road", StringComparison.OrdinalIgnoreCase)))
            || (string.Equals(t.SubType, "OOG", StringComparison.OrdinalIgnoreCase) && string.Equals(t.Mode, "Road", StringComparison.OrdinalIgnoreCase))
            || (string.Equals(t.SubType, "FCL", StringComparison.OrdinalIgnoreCase) && string.Equals(t.Mode, "Sea", StringComparison.OrdinalIgnoreCase))
            || (string.Equals(t.SubType, "LCL", StringComparison.OrdinalIgnoreCase) && string.Equals(t.Mode, "Sea", StringComparison.OrdinalIgnoreCase))
            || (string.Equals(t.SubType, "FCL", StringComparison.OrdinalIgnoreCase) && string.Equals(t.Mode, "Rail", StringComparison.OrdinalIgnoreCase));
        var (fillTitle, volLabel, volTooltip, chargeLabel) = GetFillDimensionsConfig(t.Mode);
        return new(t.Id, t.Code, t.Name, t.Direction, t.Mode, t.SubType, t.RequestNumberPrefix, t.CarrierApiPath, t.CarrierLabel, t.SortOrder, t.IsActive, supportsVas, fillTitle, volLabel, volTooltip, chargeLabel);
    }

    /// <summary>Generic fill dimension uses Air rules (1 CBM = 166.67 kg).</summary>
    private static (string Title, string VolumetricWeightLabel, string VolumetricWeightTooltip, string ChargeableWeightLabel) GetFillDimensionsConfig(string? mode)
    {
        var m = (mode ?? "").Trim();
        if (string.Equals(m, "Sea", StringComparison.OrdinalIgnoreCase))
            return ("Fill Dimensions (Sea – Ocean 1 CBM = 1 MT)", "Volumetric Weight (MT)", "Ocean 1 CBM = 1 MT. Volumetric weight = Volume × 1 MT", "Chargeable Weight (MT)");
        if (string.Equals(m, "Road", StringComparison.OrdinalIgnoreCase))
            return ("Fill Dimensions (Road – 1 CBM = 1 MT)", "Volumetric Weight (MT)", "Road 1 CBM = 1 MT. Volumetric weight = Volume × 1 MT", "Chargeable Weight (MT)");
        if (string.Equals(m, "Rail", StringComparison.OrdinalIgnoreCase))
            return ("Fill Dimensions (Rail – 1 CBM = 1 MT)", "Volumetric Weight (MT)", "Rail 1 CBM = 1 MT. Volumetric weight = Volume × 1 MT", "Chargeable Weight (MT)");
        // Air and Generic: 1 CBM = 166.67 kg
        return ("Fill Dimensions (Air – 1 CBM = 166.67 kg)", "Volumetric Weight (KG)", "Volume × 166.67 kg", "Chargeable Weight (KG)");
    }
}

using Quotes.Application.DTOs.Address;
using Quotes.Application.DTOs.Quote;
using Quotes.Domain.AggregatesModel.AddressAggregate;
using Quotes.Domain.AggregatesModel.QuoteAggregate;

namespace Quotes.Application.Features.Quotes;

public static class QuoteMapper
{
    public static QuoteDto MapToDto(QuoteEntity entity, QuoteType? quoteType)
    {
        return new QuoteDto(
            entity.Id,
            entity.CreationDate,
            entity.QuoteNumber,
            entity.QuoteTypeId,
            quoteType?.Code,
            quoteType?.Name,
            quoteType?.Direction,
            quoteType?.Mode,
            quoteType?.SubType,
            entity.CompanyId,
            entity.CompanyName,
            entity.ManagerId,
            entity.ManagerName,
            entity.LogisticianId,
            entity.LogisticianName,
            entity.HandlerId,
            entity.HandlerName,
            entity.AccountManagerId,
            entity.AccountManagerName,
            entity.OpenedById,
            entity.OpenedByName,
            entity.ManagerUserId,
            entity.HandlerUserId,
            entity.AccountManagerUserId,
            entity.OpenedByUserId,
            entity.DepartmentId,
            entity.DepartmentName,
            entity.QuoteStatus,
            entity.ShipperId,
            entity.ShipperName,
            entity.ConsigneeId,
            entity.ConsigneeName,
            entity.MyCustomerTypeId,
            entity.MyCustomerTypeName,
            entity.RateType,
            entity.StartDate,
            entity.Etd,
            entity.Eta,
            entity.IncotermId,
            entity.IncotermName,
            entity.ExpirationDays,
            entity.PurchaseFreeDays,
            entity.SaleFreeDays,
            entity.MoveTypeId,
            entity.MoveTypeName,
            entity.ExpirationDate,
            entity.CloseAutomaticallyDeclined,
            entity.CloseAutomaticallyDeclinedDays,
            entity.AutomaticallyCloseDate,
            entity.IncludeInsurance,
            entity.InsuranceValue,
            entity.IsStackable,
            entity.IncludeImportDutyCharges,
            entity.TransitTime,
            entity.IsFreighter,
            entity.DepartureFrequency,
            entity.ValueOfGoods,
            entity.PriceStandard,
            entity.RmbVwt,
            entity.CurrencyId,
            entity.CurrencyCode,
            entity.PriceWithVat,
            entity.MinVat,
            entity.VatRate,
            entity.VatNote,
            entity.IncludePickup,
            entity.PickupAddressId,
            entity.PickupAddress == null ? null : MapAddress(entity.PickupAddress),
            entity.PickupCountryName,
            entity.PickupStateName,
            entity.PickupCityName,
            entity.PickupZipCode,
            entity.GatewayTerminalId,
            entity.GatewayName,
            entity.ViaPortTerminalId,
            entity.ViaPortName,
            entity.DestinationTerminalId,
            entity.DestinationName,
            entity.ViaPort2TerminalId,
            entity.ViaPort2Name,
            entity.CarrierId,
            entity.CarrierName,
            entity.MyPortTerminalId,
            entity.MyPortName,
            entity.MyPort2TerminalId,
            entity.MyPort2Name,
            entity.PortOfDeliveryName,
            entity.VasId,
            entity.IncludeVas,
            entity.VasServiceName,
            entity.ExecutionPlace,
            entity.VasQuantity,
            entity.VasUom,
            entity.VasCurrencyCode,
            entity.VasTotal,
            entity.VasNotes,
            entity.IncludeDelivery,
            entity.DeliveryAddressId,
            entity.DeliveryAddress == null ? null : MapAddress(entity.DeliveryAddress),
            entity.DeliveryCountryName,
            entity.DeliveryStateName,
            entity.DeliveryCityName,
            entity.DeliveryZipCode,
            entity.GrossWeightKg,
            entity.VolumeCbm,
            entity.ChargeableWeightKg,
            entity.NumberOfPackages,
            entity.DangerousGoods,
            entity.DescriptionOfGoods,
            entity.Quantity1,
            entity.Quantity2,
            entity.Quantity3,
            entity.Quantity4,
            entity.PackageType1,
            entity.PackageType2,
            entity.PackageType3,
            entity.PackageType4,
            entity.Quantity5,
            entity.PackageType5,
            entity.ShipperRef2,
            entity.ConsigneeRef2,
            entity.AgentId,
            entity.AgentName,
            entity.NotesToBePrinted,
            entity.Notes,
            entity.SentToCustomerAt,
            entity.IsCancelled,
            entity.CancelledAt,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt);
    }

    private static AddressDto MapAddress(Address a) => new(
        a.Id, a.AddressTypeId, a.AddressTypeName, a.Description, a.Name, a.Address1, a.Address2,
        a.CountryId, a.CountryName, a.Phone, a.StateId, a.StateName, a.Fax, a.CityId, a.CityName,
        a.Attn, a.ZipCode, a.Notes, a.FullAddressDisplay);

    public static QuoteTypeDto MapTypeToDto(QuoteType t)
    {
        var (fillTitle, volLabel, volTooltip, chargeLabel) = GetFillDimensionsConfig(t.Mode);
        return new QuoteTypeDto(t.Id, t.Code, t.Name, t.Direction, t.Mode, t.SubType, t.QuoteNumberPrefix,
            t.CarrierApiPath, t.CarrierLabel, t.SortOrder, t.IsActive, fillTitle, volLabel, volTooltip, chargeLabel);
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

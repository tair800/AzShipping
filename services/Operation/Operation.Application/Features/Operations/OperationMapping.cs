using Operation.Application.DTOs.Operation;
using Operation.Application.Services;
using Operation.Domain.AggregatesModel.OperationAggregate;

namespace Operation.Application.Features.Operations;

public static class OperationMapping
{
    public static OperationDto ToDto(LogisticsOperation e, OperationType? t, IReadOnlyList<OperationDimension> dims, IReadOnlyList<OperationPackageLine> pkgLines, IReadOnlyList<OperationVas> vasItems) => new(
        e.Id, e.CreationDate, e.OperationNumber, e.OperationTypeId, t?.Code, t?.Name,
        e.ModalType, e.PricingMode, e.ClientOrderNumber,
        e.CompanyId, e.CompanyName, e.ManagerId, e.ManagerName, e.LogisticianId, e.LogisticianName,
        e.DepartmentId, e.DepartmentName, e.ShipperId, e.ShipperName, e.ConsigneeId, e.ConsigneeName,
        e.MyCustomerParty, e.MyCustomerClientId, e.MyCustomerName, e.StartDate, e.StartTime,
        e.IncotermId, e.IncotermName, e.FreightPrepaidCollect, e.MoveTypeName, e.OtherPrepaidCollect,
        e.SalesmanId, e.SalesmanName, e.PriceStandard, e.CurrencyId, e.CurrencyCode, e.PriceWithVat, e.VatRate,
        e.DeferredPaymentConditionId, e.DeferredPaymentConditionName, e.DeferredPaymentDays,
        e.IncludePickup, e.PickupCountryId, e.PickupCountryName, e.PickupStateId, e.PickupStateName,
        e.PickupCityId, e.PickupCityName, e.PickupZipCode,
        e.GatewayTerminalId, e.GatewayName, e.ViaPortTerminalId, e.ViaPortName, e.DestinationTerminalId, e.DestinationName,
        e.ViaPort2TerminalId, e.ViaPort2Name, e.CarrierId, e.CarrierName, e.FlightNumber, e.Mawb,
        e.IncludeDelivery, e.DeliveryCountryId, e.DeliveryCountryName, e.DeliveryStateId, e.DeliveryStateName,
        e.DeliveryCityId, e.DeliveryCityName, e.DeliveryZipCode,
        e.GrossWeightKg, e.VolumeCbm, e.ChargeableWeightKg, e.NumberOfPackages, e.DangerousGoods, e.DescriptionOfGoods,
        e.AgentId, e.AgentName, e.Reference1, e.Reference2, e.MainHarmonize, e.NotesToBePrinted, e.TrackingNumber,
        e.TemplateId, e.TemplateName, e.Notes, e.IsActive, e.OperationStageName, e.CreatedAt, e.UpdatedAt,
        e.CargoName, e.LoadingMethodId, e.LoadingMethodName, e.CargoTransportTypeId, e.CargoTransportTypeName,
        e.ConsignmentPrice, e.ConsignmentCurrencyId, e.ConsignmentCurrencyCode, e.CargoAdditionalInformation,
        e.PortOfDeliveryTerminalId, e.PortOfDeliveryName, e.OceanBillOfLading, e.VesselId, e.VesselName,
        e.RoadTruckerNumber, e.RoadWaybillNumber,
        e.IncludeVas,
        vasItems.Select(v => new OperationVasDto(v.Id, v.VasId, v.VasName, v.ExecutionPlace, v.Quantity, v.Uom, v.CurrencyId, v.CurrencyCode, v.Total, v.Notes)).ToList(),
        pkgLines.OrderBy(x => x.SortOrder).Select(p => new OperationPackageLineDto(p.Id, p.Quantity, p.PackageType, p.SortOrder)).ToList(),
        dims.Select(d => new OperationDimensionDto(d.Id, d.Length, d.Width, d.Height, d.Quantity, d.WeightKg, d.VolumeCbm, d.PackageType)).ToList());

    public static OperationTypeDto ToTypeDto(OperationType t) => new(
        t.Id, t.Code, t.Name, t.Direction, t.Mode, t.SubType, t.OperationNumberPrefix,
        t.CarrierApiPath, t.CarrierLabel, t.SortOrder, t.IsActive);

    public static LogisticsOperation CreateEntity(SaveOperationDto d, Guid id, DateTime now, Guid operationTypeId)
    {
        var e = new LogisticsOperation
        {
            Id = id,
            CreationDate = now,
            CreatedAt = now,
            OperationNumber = d.OperationNumber!.Trim(),
            OperationTypeId = operationTypeId,
            ModalType = string.IsNullOrWhiteSpace(d.ModalType) ? "Unimodal" : d.ModalType.Trim(),
            PricingMode = string.IsNullOrWhiteSpace(d.PricingMode) ? "RoutingRates" : d.PricingMode.Trim(),
            OperationStageName = "Planning",
        };
        return e;
    }

    public static void MergeScalars(LogisticsOperation e, SaveOperationDto d)
    {
        if (d.ModalType != null) e.ModalType = string.IsNullOrWhiteSpace(d.ModalType) ? e.ModalType : d.ModalType.Trim();
        if (d.PricingMode != null) e.PricingMode = string.IsNullOrWhiteSpace(d.PricingMode) ? e.PricingMode : d.PricingMode.Trim();
        if (d.OperationNumber != null) e.OperationNumber = d.OperationNumber.Trim();
        if (d.ClientOrderNumber != null) e.ClientOrderNumber = d.ClientOrderNumber;
        if (d.CompanyId != null) e.CompanyId = d.CompanyId;
        if (d.CompanyName != null) e.CompanyName = d.CompanyName;
        if (d.ManagerId != null) e.ManagerId = d.ManagerId;
        if (d.ManagerName != null) e.ManagerName = d.ManagerName;
        if (d.LogisticianId != null) e.LogisticianId = d.LogisticianId;
        if (d.LogisticianName != null) e.LogisticianName = d.LogisticianName;
        if (d.DepartmentId != null) e.DepartmentId = d.DepartmentId;
        if (d.DepartmentName != null) e.DepartmentName = d.DepartmentName;
        if (d.ShipperId != null) e.ShipperId = d.ShipperId;
        if (d.ShipperName != null) e.ShipperName = d.ShipperName;
        if (d.ConsigneeId != null) e.ConsigneeId = d.ConsigneeId;
        if (d.ConsigneeName != null) e.ConsigneeName = d.ConsigneeName;
        if (d.MyCustomerParty != null) e.MyCustomerParty = d.MyCustomerParty;
        if (d.MyCustomerClientId != null) e.MyCustomerClientId = d.MyCustomerClientId;
        if (d.MyCustomerName != null) e.MyCustomerName = d.MyCustomerName;
        if (d.StartDate != null) e.StartDate = d.StartDate;
        if (d.StartTime != null) e.StartTime = d.StartTime;
        if (d.IncotermId != null) e.IncotermId = d.IncotermId;
        if (d.IncotermName != null) e.IncotermName = d.IncotermName;
        if (d.FreightPrepaidCollect != null) e.FreightPrepaidCollect = d.FreightPrepaidCollect;
        if (d.MoveTypeName != null) e.MoveTypeName = d.MoveTypeName;
        if (d.OtherPrepaidCollect != null) e.OtherPrepaidCollect = d.OtherPrepaidCollect;
        if (d.SalesmanId != null) e.SalesmanId = d.SalesmanId;
        if (d.SalesmanName != null) e.SalesmanName = d.SalesmanName;
        if (d.PriceStandard != null) e.PriceStandard = d.PriceStandard;
        if (d.CurrencyId != null) e.CurrencyId = d.CurrencyId;
        if (d.CurrencyCode != null) e.CurrencyCode = d.CurrencyCode;
        if (d.PriceWithVat != null) e.PriceWithVat = d.PriceWithVat;
        if (d.VatRate != null) e.VatRate = d.VatRate;
        if (d.DeferredPaymentConditionId != null) e.DeferredPaymentConditionId = d.DeferredPaymentConditionId;
        if (d.DeferredPaymentConditionName != null) e.DeferredPaymentConditionName = d.DeferredPaymentConditionName;
        if (d.DeferredPaymentDays != null) e.DeferredPaymentDays = d.DeferredPaymentDays;
        if (d.IncludePickup.HasValue) e.IncludePickup = d.IncludePickup.Value;
        if (d.PickupCountryId != null) e.PickupCountryId = d.PickupCountryId;
        if (d.PickupCountryName != null) e.PickupCountryName = d.PickupCountryName;
        if (d.PickupStateId != null) e.PickupStateId = d.PickupStateId;
        if (d.PickupStateName != null) e.PickupStateName = d.PickupStateName;
        if (d.PickupCityId != null) e.PickupCityId = d.PickupCityId;
        if (d.PickupCityName != null) e.PickupCityName = d.PickupCityName;
        if (d.PickupZipCode != null) e.PickupZipCode = d.PickupZipCode;
        if (d.GatewayTerminalId != null) e.GatewayTerminalId = d.GatewayTerminalId;
        if (d.GatewayName != null) e.GatewayName = d.GatewayName;
        if (d.ViaPortTerminalId != null) e.ViaPortTerminalId = d.ViaPortTerminalId;
        if (d.ViaPortName != null) e.ViaPortName = d.ViaPortName;
        if (d.DestinationTerminalId != null) e.DestinationTerminalId = d.DestinationTerminalId;
        if (d.DestinationName != null) e.DestinationName = d.DestinationName;
        if (d.ViaPort2TerminalId != null) e.ViaPort2TerminalId = d.ViaPort2TerminalId;
        if (d.ViaPort2Name != null) e.ViaPort2Name = d.ViaPort2Name;
        if (d.CarrierId != null) e.CarrierId = d.CarrierId;
        if (d.CarrierName != null) e.CarrierName = d.CarrierName;
        if (d.FlightNumber != null) e.FlightNumber = d.FlightNumber;
        if (d.Mawb != null) e.Mawb = d.Mawb;
        if (d.PortOfDeliveryTerminalId != null) e.PortOfDeliveryTerminalId = d.PortOfDeliveryTerminalId;
        if (d.PortOfDeliveryName != null) e.PortOfDeliveryName = d.PortOfDeliveryName;
        if (d.OceanBillOfLading != null) e.OceanBillOfLading = d.OceanBillOfLading;
        if (d.VesselId != null) e.VesselId = d.VesselId;
        if (d.VesselName != null) e.VesselName = d.VesselName;
        if (d.RoadTruckerNumber != null) e.RoadTruckerNumber = d.RoadTruckerNumber;
        if (d.RoadWaybillNumber != null) e.RoadWaybillNumber = d.RoadWaybillNumber;
        if (d.IncludeDelivery.HasValue) e.IncludeDelivery = d.IncludeDelivery.Value;
        if (d.DeliveryCountryId != null) e.DeliveryCountryId = d.DeliveryCountryId;
        if (d.DeliveryCountryName != null) e.DeliveryCountryName = d.DeliveryCountryName;
        if (d.DeliveryStateId != null) e.DeliveryStateId = d.DeliveryStateId;
        if (d.DeliveryStateName != null) e.DeliveryStateName = d.DeliveryStateName;
        if (d.DeliveryCityId != null) e.DeliveryCityId = d.DeliveryCityId;
        if (d.DeliveryCityName != null) e.DeliveryCityName = d.DeliveryCityName;
        if (d.DeliveryZipCode != null) e.DeliveryZipCode = d.DeliveryZipCode;
        if (d.GrossWeightKg != null) e.GrossWeightKg = d.GrossWeightKg;
        if (d.VolumeCbm != null) e.VolumeCbm = d.VolumeCbm;
        if (d.ChargeableWeightKg != null) e.ChargeableWeightKg = d.ChargeableWeightKg;
        if (d.NumberOfPackages != null) e.NumberOfPackages = d.NumberOfPackages;
        if (d.DangerousGoods.HasValue) e.DangerousGoods = d.DangerousGoods.Value;
        if (d.DescriptionOfGoods != null) e.DescriptionOfGoods = d.DescriptionOfGoods;
        if (d.AgentId != null) e.AgentId = d.AgentId;
        if (d.AgentName != null) e.AgentName = d.AgentName;
        if (d.Reference1 != null) e.Reference1 = d.Reference1;
        if (d.Reference2 != null) e.Reference2 = d.Reference2;
        if (d.MainHarmonize != null) e.MainHarmonize = d.MainHarmonize;
        if (d.NotesToBePrinted != null) e.NotesToBePrinted = d.NotesToBePrinted;
        if (d.TrackingNumber != null) e.TrackingNumber = d.TrackingNumber;
        if (d.TemplateId != null) e.TemplateId = d.TemplateId;
        if (d.TemplateName != null) e.TemplateName = d.TemplateName;
        if (d.Notes != null) e.Notes = d.Notes;
        if (d.IsActive.HasValue) e.IsActive = d.IsActive.Value;
        if (d.OperationStageName != null) e.OperationStageName = d.OperationStageName;
        if (d.CargoName != null) e.CargoName = d.CargoName;
        if (d.LoadingMethodId != null) e.LoadingMethodId = d.LoadingMethodId;
        if (d.LoadingMethodName != null) e.LoadingMethodName = d.LoadingMethodName;
        if (d.CargoTransportTypeId != null) e.CargoTransportTypeId = d.CargoTransportTypeId;
        if (d.CargoTransportTypeName != null) e.CargoTransportTypeName = d.CargoTransportTypeName;
        if (d.ConsignmentPrice != null) e.ConsignmentPrice = d.ConsignmentPrice;
        if (d.ConsignmentCurrencyId != null) e.ConsignmentCurrencyId = d.ConsignmentCurrencyId;
        if (d.ConsignmentCurrencyCode != null) e.ConsignmentCurrencyCode = d.ConsignmentCurrencyCode;
        if (d.CargoAdditionalInformation != null) e.CargoAdditionalInformation = d.CargoAdditionalInformation;
        if (d.IncludeVas.HasValue) e.IncludeVas = d.IncludeVas.Value;
    }

    public static bool OperationTypeSupportsVas(OperationType? type)
    {
        if (type == null) return false;
        if (string.Equals(type.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase))
            return string.Equals(type.Mode, "Sea", StringComparison.OrdinalIgnoreCase)
                || string.Equals(type.Mode, "Road", StringComparison.OrdinalIgnoreCase)
                || string.Equals(type.Mode, "Rail", StringComparison.OrdinalIgnoreCase);
        if (string.Equals(type.SubType, "OOG", StringComparison.OrdinalIgnoreCase))
            return string.Equals(type.Mode, "Road", StringComparison.OrdinalIgnoreCase);
        return false;
    }

    /// <summary>Sum package line quantities into <see cref="LogisticsOperation.NumberOfPackages"/> for Sea/Rail FCL &amp; breakbulk or Road FTL/breakbulk.</summary>
    public static void ApplySeaPackageTotals(LogisticsOperation e, OperationType? type, SaveOperationDto d)
    {
        if (type == null) return;
        var useLines =
            (string.Equals(type.Mode, "Sea", StringComparison.OrdinalIgnoreCase)
             && (string.Equals(type.SubType, "FCL", StringComparison.OrdinalIgnoreCase)
                 || string.Equals(type.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase)))
            || (string.Equals(type.Mode, "Rail", StringComparison.OrdinalIgnoreCase)
                && (string.Equals(type.SubType, "FCL", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(type.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase)))
            || (string.Equals(type.Mode, "Road", StringComparison.OrdinalIgnoreCase)
                && (string.Equals(type.SubType, "FTL", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(type.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase)));
        if (!useLines) return;
        if (d.PackageLines is not { Count: > 0 })
            return;
        var sum = 0;
        foreach (var x in d.PackageLines)
            sum += Math.Max(0, x.Quantity);
        if (sum > 0)
            e.NumberOfPackages = sum;
    }

    /// <summary>Apply air volumetric rules for Air; Sea LCL, Rail LCL, and Road LTL/OOG use 1000 kg/CBM (W/M style).</summary>
    public static void ApplyAirCargoFromInputs(LogisticsOperation e, OperationType? type, SaveOperationDto d)
    {
        if (type == null) return;
        var air = string.Equals(type.Mode, "Air", StringComparison.OrdinalIgnoreCase);
        var seaLcl = string.Equals(type.Mode, "Sea", StringComparison.OrdinalIgnoreCase)
            && string.Equals(type.SubType, "LCL", StringComparison.OrdinalIgnoreCase);
        var railLcl = string.Equals(type.Mode, "Rail", StringComparison.OrdinalIgnoreCase)
            && string.Equals(type.SubType, "LCL", StringComparison.OrdinalIgnoreCase);
        var roadLclStyle = string.Equals(type.Mode, "Road", StringComparison.OrdinalIgnoreCase)
            && (string.Equals(type.SubType, "LTL", StringComparison.OrdinalIgnoreCase)
                || string.Equals(type.SubType, "OOG", StringComparison.OrdinalIgnoreCase));
        if (!air && !seaLcl && !railLcl && !roadLclStyle) return;

        var volFactor = seaLcl || railLcl || roadLclStyle
            ? AirFreightCalculationService.SeaLclVolumetricFactorKgPerCbm
            : AirFreightCalculationService.VolumetricFactorKgPerCbm;

        if (d.Dimensions is { Count: > 0 })
        {
            var inputs = d.Dimensions.Select(x => new AirFreightCalculationService.DimensionInput(x.Length, x.Width, x.Height, x.Quantity, x.WeightKg, x.VolumeCbm)).ToList();
            var t = AirFreightCalculationService.CalculateTotals(inputs, volFactor);
            e.GrossWeightKg = t.TotalGrossWeightKg > 0 ? t.TotalGrossWeightKg : e.GrossWeightKg;
            e.VolumeCbm = t.TotalVolumeCbm > 0 ? t.TotalVolumeCbm : e.VolumeCbm;
            e.ChargeableWeightKg = t.ChargeableWeightKg > 0 ? t.ChargeableWeightKg : e.ChargeableWeightKg;
            e.NumberOfPackages = t.NumberOfPackages > 0 ? t.NumberOfPackages : e.NumberOfPackages;
        }
        else if (d.VolumeCbm is > 0)
        {
            e.VolumeCbm = d.VolumeCbm;
            e.ChargeableWeightKg = AirFreightCalculationService.CalculateChargeableFromManualVolume(d.VolumeCbm, volFactor);
        }
    }

    public static IReadOnlyList<OperationDimension> BuildDimensionEntities(Guid operationId, SaveOperationDto d)
    {
        if (d.Dimensions is not { Count: > 0 }) return [];
        return d.Dimensions.Select(x =>
        {
            var qty = Math.Max(1, x.Quantity);
            var vol = AirFreightCalculationService.GetLineVolumeCbm(x.Length, x.Width, x.Height, qty, x.VolumeCbm);
            return new OperationDimension
            {
                Id = Guid.NewGuid(),
                OperationId = operationId,
                Length = x.Length,
                Width = x.Width,
                Height = x.Height,
                Quantity = qty,
                WeightKg = x.WeightKg,
                VolumeCbm = vol,
                PackageType = x.PackageType
            };
        }).ToList();
    }

    public static IReadOnlyList<OperationPackageLine> BuildPackageLineEntities(Guid operationId, SaveOperationDto d)
    {
        if (d.PackageLines is not { Count: > 0 }) return [];
        var order = 0;
        var list = new List<OperationPackageLine>();
        foreach (var x in d.PackageLines)
        {
            if (x.Quantity <= 0) continue;
            var pt = string.IsNullOrWhiteSpace(x.PackageType) ? null : x.PackageType.Trim();
            list.Add(new OperationPackageLine
            {
                Id = Guid.NewGuid(),
                OperationId = operationId,
                Quantity = x.Quantity,
                PackageType = pt,
                SortOrder = order++
            });
        }

        return list;
    }

    public static IReadOnlyList<OperationVas> BuildVasEntities(Guid operationId, SaveOperationDto d, OperationType? type)
    {
        if (!OperationTypeSupportsVas(type) || d is not { IncludeVas: true } || d.VasItems is not { Count: > 0 })
            return [];
        var list = new List<OperationVas>();
        foreach (var x in d.VasItems)
        {
            if (x.VasId == Guid.Empty) continue;
            list.Add(new OperationVas
            {
                Id = Guid.NewGuid(),
                OperationId = operationId,
                VasId = x.VasId,
                VasName = string.IsNullOrWhiteSpace(x.VasName) ? null : x.VasName.Trim(),
                ExecutionPlace = string.IsNullOrWhiteSpace(x.ExecutionPlace) ? null : x.ExecutionPlace.Trim(),
                Quantity = x.Quantity,
                Uom = string.IsNullOrWhiteSpace(x.Uom) ? null : x.Uom.Trim(),
                CurrencyId = x.CurrencyId,
                CurrencyCode = string.IsNullOrWhiteSpace(x.CurrencyCode) ? null : x.CurrencyCode.Trim(),
                Total = x.Total,
                Notes = string.IsNullOrWhiteSpace(x.Notes) ? null : x.Notes.Trim()
            });
        }

        return list;
    }

    public static OperationListItemDto ToListItemDto(LogisticsOperation e, OperationType? t)
    {
        static string? FirstNonEmpty(params string?[] values)
        {
            foreach (var v in values)
            {
                if (!string.IsNullOrWhiteSpace(v)) return v!.Trim();
            }
            return null;
        }

        var client = !string.IsNullOrWhiteSpace(e.MyCustomerName)
            ? e.MyCustomerName
            : (!string.IsNullOrWhiteSpace(e.ShipperName) ? e.ShipperName : e.CompanyName);
        var routeNames = new List<string>();
        foreach (var n in new[] { e.GatewayName, e.ViaPortName, e.ViaPort2Name, e.DestinationName, e.PortOfDeliveryName })
        {
            if (string.IsNullOrWhiteSpace(n)) continue;
            var p = n.Trim();
            if (!routeNames.Contains(p)) routeNames.Add(p);
        }

        var route = routeNames.Count > 0 ? string.Join(" → ", routeNames) : null;
        var trip = FirstNonEmpty(e.TrackingNumber, e.FlightNumber, e.Mawb, e.OceanBillOfLading, e.RoadWaybillNumber,
            e.RoadTruckerNumber);

        var cargoBits = new List<string>();
        if (!string.IsNullOrWhiteSpace(e.CargoName)) cargoBits.Add(e.CargoName!);
        if (!string.IsNullOrWhiteSpace(e.DescriptionOfGoods)) cargoBits.Add(e.DescriptionOfGoods!);
        var cargoSummary = cargoBits.Count > 0 ? string.Join(" · ", cargoBits) : null;

        OperationDimension? firstDim = null;
        foreach (var d in e.Dimensions)
        {
            firstDim = d;
            break;
        }

        string? dimLabel = null;
        if (firstDim != null)
            dimLabel = $"{firstDim.Length}x{firstDim.Width}x{firstDim.Height}";

        var opCur = string.IsNullOrWhiteSpace(e.CurrencyCode) ? null : e.CurrencyCode.Trim();
        var vasLines = new List<OperationListExpensePreviewDto>();
        decimal vasSumSame = 0;
        foreach (var v in e.VasItems.OrderBy(x => x.Id))
        {
            var desc = !string.IsNullOrWhiteSpace(v.VasName) ? v.VasName : v.Notes;
            var vc = string.IsNullOrWhiteSpace(v.CurrencyCode) ? opCur : v.CurrencyCode.Trim();
            vasLines.Add(new OperationListExpensePreviewDto(desc, v.Total, vc));
            if (v.Total is > 0 && opCur != null &&
                string.Equals(vc, opCur, StringComparison.OrdinalIgnoreCase))
                vasSumSame += v.Total.Value;
        }

        var freight = e.PriceStandard ?? e.ConsignmentPrice;
        decimal? profit = null;
        if (freight.HasValue && opCur != null)
            profit = Math.Round(freight.Value - vasSumSame, 2, MidpointRounding.AwayFromZero);

        var status = string.IsNullOrWhiteSpace(e.OperationStageName) ? "Planning" : e.OperationStageName.Trim();

        return new OperationListItemDto(
            e.Id,
            e.CreationDate,
            e.OperationNumber,
            status,
            t?.Code,
            t?.Name,
            t?.Direction,
            t?.Mode,
            t?.SubType,
            e.ModalType,
            client,
            e.CarrierName,
            route,
            trip,
            cargoSummary,
            e.VolumeCbm,
            e.GrossWeightKg,
            dimLabel,
            freight,
            opCur,
            vasLines,
            vasSumSame,
            opCur,
            profit,
            HasDocuments: false);
    }

    public static TripListItemDto ToTripListItemDto(LogisticsOperation e, OperationType? t)
    {
        static string? FirstNonEmpty(params string?[] values)
        {
            foreach (var v in values)
            {
                if (!string.IsNullOrWhiteSpace(v)) return v!.Trim();
            }
            return null;
        }

        static string? FormatPlace(string? country, string? state, string? city)
        {
            var parts = new List<string>();
            foreach (var x in new[] { country, state, city })
            {
                if (string.IsNullOrWhiteSpace(x)) continue;
                var p = x.Trim();
                if (!parts.Contains(p)) parts.Add(p);
            }
            return parts.Count > 0 ? string.Join(", ", parts) : null;
        }

        var client = !string.IsNullOrWhiteSpace(e.MyCustomerName)
            ? e.MyCustomerName
            : (!string.IsNullOrWhiteSpace(e.ShipperName) ? e.ShipperName : e.CompanyName);

        var loadingPlace = FormatPlace(e.PickupCountryName, e.PickupStateName, e.PickupCityName);
        var unloadingPlace = FormatPlace(e.DeliveryCountryName, e.DeliveryStateName, e.DeliveryCityName);

        var tripCore = FirstNonEmpty(e.TrackingNumber, e.FlightNumber, e.Mawb, e.OceanBillOfLading,
            e.RoadWaybillNumber, e.RoadTruckerNumber);
        var tripRef = tripCore != null ? tripCore : $"Trips {e.OperationNumber}";

        var cargoParts = new List<string>();
        if (!string.IsNullOrWhiteSpace(e.CargoName)) cargoParts.Add(e.CargoName!);
        if (!string.IsNullOrWhiteSpace(e.DescriptionOfGoods)) cargoParts.Add(e.DescriptionOfGoods!);
        if (e.GrossWeightKg is > 0) cargoParts.Add($"Weight: {e.GrossWeightKg:0.###}");
        if (e.VolumeCbm is > 0) cargoParts.Add($"Vol: {e.VolumeCbm:0.###}");
        var cargoParam = cargoParts.Count > 0 ? string.Join(" · ", cargoParts) : null;

        var cargoNo = FirstNonEmpty(e.Reference1, e.ClientOrderNumber, e.Reference2);
        var status = string.IsNullOrWhiteSpace(e.OperationStageName) ? "Planning" : e.OperationStageName.Trim();
        var attrs = e.DangerousGoods ? "DG" : null;

        return new TripListItemDto(
            e.Id,
            e.CreationDate,
            e.OperationNumber,
            t?.Direction,
            t?.Mode,
            t?.SubType,
            e.ModalType,
            e.CompanyName,
            client,
            e.StartDate,
            UnloadingDate: null,
            e.ShipperName,
            loadingPlace,
            e.ConsigneeName,
            unloadingPlace,
            status,
            FirstNonEmpty(e.GatewayName, e.IncotermName, e.MoveTypeName),
            e.StartDate,
            e.StartTime,
            e.Notes,
            cargoNo,
            string.IsNullOrWhiteSpace(e.CargoName) ? null : e.CargoName.Trim(),
            cargoParam,
            attrs,
            e.CarrierName,
            tripRef);
    }
}

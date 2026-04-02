using MediatR;
using Request.Application.DTOs.Request;
using Request.Application.Features.Requests;
using Request.Application.Services;
using Request.Application.Validation;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Features.Requests.Commands.Update;

public sealed class UpdateRequestCommandHandler(
    IRequestRepository repository,
    IRequestTypeRepository typeRepository,
    IRequestDimensionRepository dimensionRepository,
    IRequestVasRepository vasRepository,
    IActionLogClient actionLogClient) : IRequestHandler<UpdateRequestCommand, RequestDto?>
{
    public async Task<RequestDto?> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        var d = request.Dto;
        if (d.RequestNumber != null) entity.RequestNumber = d.RequestNumber;
        if (d.RequestTypeId.HasValue) entity.RequestTypeId = d.RequestTypeId.Value;
        entity.CompanyId = d.CompanyId;
        if (d.CompanyName != null) entity.CompanyName = d.CompanyName;
        entity.ManagerId = d.ManagerId;
        if (d.ManagerName != null) entity.ManagerName = d.ManagerName;
        entity.LogisticianId = d.LogisticianId;
        if (d.LogisticianName != null) entity.LogisticianName = d.LogisticianName;
        entity.DepartmentId = d.DepartmentId;
        if (d.DepartmentName != null) entity.DepartmentName = d.DepartmentName;
        entity.ShipperId = d.ShipperId;
        if (d.ShipperName != null) entity.ShipperName = d.ShipperName;
        entity.ConsigneeId = d.ConsigneeId;
        if (d.ConsigneeName != null) entity.ConsigneeName = d.ConsigneeName;
        entity.MyCustomerTypeId = d.MyCustomerTypeId;
        if (d.MyCustomerTypeName != null) entity.MyCustomerTypeName = d.MyCustomerTypeName;
        if (d.DispatchDateFrom != null) entity.DispatchDateFrom = d.DispatchDateFrom;
        if (d.DispatchDateTo != null) entity.DispatchDateTo = d.DispatchDateTo;
        if (d.UnloadingDateFrom != null) entity.UnloadingDateFrom = d.UnloadingDateFrom;
        if (d.UnloadingDateTo != null) entity.UnloadingDateTo = d.UnloadingDateTo;
        if (d.QuotationSent != null) entity.QuotationSent = d.QuotationSent;
        entity.StatusName = d.StatusName;
        if (d.ExtremelyUrgent != null) entity.ExtremelyUrgent = d.ExtremelyUrgent.Value;
        if (d.ToAnswerUntilDate != null) entity.ToAnswerUntilDate = d.ToAnswerUntilDate;
        if (d.PriceStandard != null) entity.PriceStandard = d.PriceStandard;
        entity.CurrencyId = d.CurrencyId;
        if (d.CurrencyCode != null) entity.CurrencyCode = d.CurrencyCode;
        if (d.PriceWithVat != null) entity.PriceWithVat = d.PriceWithVat;
        entity.VatRate = d.VatRate;
        entity.SourceOfRequestId = d.SourceOfRequestId;
        if (d.SourceOfRequestName != null) entity.SourceOfRequestName = d.SourceOfRequestName;
        entity.RequestPurposeId = d.RequestPurposeId;
        if (d.RequestPurposeName != null) entity.RequestPurposeName = d.RequestPurposeName;
        entity.GatewayTerminalId = d.GatewayTerminalId;
        if (d.GatewayName != null) entity.GatewayName = d.GatewayName;
        entity.ViaPortTerminalId = d.ViaPortTerminalId;
        if (d.ViaPortName != null) entity.ViaPortName = d.ViaPortName;
        entity.DestinationTerminalId = d.DestinationTerminalId;
        if (d.DestinationName != null) entity.DestinationName = d.DestinationName;
        entity.ViaPort2TerminalId = d.ViaPort2TerminalId;
        if (d.ViaPort2Name != null) entity.ViaPort2Name = d.ViaPort2Name;
        var reqTypeForTransit = await typeRepository.GetByIdAsync(entity.RequestTypeId, cancellationToken);
        var supportsTransitPort = reqTypeForTransit != null && string.Equals(reqTypeForTransit.Direction, "Transit", StringComparison.OrdinalIgnoreCase)
            && (string.Equals(reqTypeForTransit.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase)
                || (string.Equals(reqTypeForTransit.Mode, "Road", StringComparison.OrdinalIgnoreCase) && (string.Equals(reqTypeForTransit.SubType, "FTL", StringComparison.OrdinalIgnoreCase) || string.Equals(reqTypeForTransit.SubType, "LTL", StringComparison.OrdinalIgnoreCase) || string.Equals(reqTypeForTransit.SubType, "OOG", StringComparison.OrdinalIgnoreCase)))
                || (string.Equals(reqTypeForTransit.Mode, "Rail", StringComparison.OrdinalIgnoreCase) && (string.Equals(reqTypeForTransit.SubType, "FCL", StringComparison.OrdinalIgnoreCase) || string.Equals(reqTypeForTransit.SubType, "LCL", StringComparison.OrdinalIgnoreCase) || string.Equals(reqTypeForTransit.SubType, "OOG", StringComparison.OrdinalIgnoreCase))));
        entity.TransitPortTerminalId = supportsTransitPort ? d.TransitPortTerminalId : null;
        entity.TransitPortName = supportsTransitPort ? d.TransitPortName : null;
        entity.CarrierId = d.CarrierId;
        if (d.CarrierName != null) entity.CarrierName = d.CarrierName;
        entity.StationOfDeliveryTerminalId = d.StationOfDeliveryTerminalId;
        if (d.StationOfDeliveryName != null) entity.StationOfDeliveryName = d.StationOfDeliveryName;
        if (d.DangerousGoods != null) entity.DangerousGoods = d.DangerousGoods.Value;
        entity.DescriptionOfGoods = d.DescriptionOfGoods;
        entity.Notes = d.Notes;
        if (d.IsActive != null) entity.IsActive = d.IsActive.Value;
        entity.UpdatedAt = DateTime.UtcNow;

        if (d.Dimensions != null)
        {
            var reqTypeForDim = await typeRepository.GetByIdAsync(d.RequestTypeId ?? entity.RequestTypeId, cancellationToken);
            RequestDimensionValidator.Validate(d.Dimensions, reqTypeForDim);
            await dimensionRepository.DeleteByRequestIdAsync(entity.Id, cancellationToken);
            if (d.Dimensions.Count > 0)
            {
                var inputs = d.Dimensions.Select(x => new ExportAirRequestCalculationService.DimensionInput(x.Length, x.Width, x.Height, x.Quantity, x.WeightKg)).ToList();
                var isSea = reqTypeForDim != null && string.Equals(reqTypeForDim.Mode, "Sea", StringComparison.OrdinalIgnoreCase);
                var isRoad = reqTypeForDim != null && string.Equals(reqTypeForDim.Mode, "Road", StringComparison.OrdinalIgnoreCase);
                var isRail = reqTypeForDim != null && string.Equals(reqTypeForDim.Mode, "Rail", StringComparison.OrdinalIgnoreCase);
                var useSeaFormula = isSea || isRoad || isRail; // Sea, Road, Rail: 1 CBM = 1 MT
                if (useSeaFormula)
                {
                    var totals = SeaFreightCalculationService.CalculateTotals(inputs);
                    entity.GrossWeightKg = totals.TotalGrossWeightKg > 0 ? totals.TotalGrossWeightKg : null;
                    entity.VolumeCbm = totals.TotalVolumeCbm > 0 ? totals.TotalVolumeCbm : null;
                    entity.ChargeableWeightKg = totals.ChargeableWeightMt > 0 ? totals.ChargeableWeightMt : null;
                    entity.NumberOfPackages = totals.NumberOfPackages > 0 ? totals.NumberOfPackages : null;
                }
                else
                {
                    var totals = ExportAirRequestCalculationService.CalculateTotals(inputs);
                    entity.GrossWeightKg = totals.TotalGrossWeightKg > 0 ? totals.TotalGrossWeightKg : null;
                    entity.VolumeCbm = totals.TotalVolumeCbm > 0 ? totals.TotalVolumeCbm : null;
                    entity.ChargeableWeightKg = totals.ChargeableWeightKg > 0 ? totals.ChargeableWeightKg : null;
                    entity.NumberOfPackages = totals.NumberOfPackages > 0 ? totals.NumberOfPackages : null;
                }

                var dims = d.Dimensions.Select(x =>
                {
                    var qty = x.PackageType != null ? Math.Max(0, x.Quantity) : Math.Max(1, x.Quantity);
                    var volCbm = ExportAirRequestCalculationService.CalculateVolumeCbm(x.Length, x.Width, x.Height, qty);
                    return new RequestDimension
                    {
                        Id = Guid.NewGuid(),
                        RequestId = entity.Id,
                        Length = x.Length, Width = x.Width, Height = x.Height,
                        Quantity = qty, WeightKg = x.WeightKg, VolumeCbm = volCbm,
                        PackageType = x.PackageType
                    };
                }).ToList();
                await dimensionRepository.AddRangeAsync(dims, cancellationToken);
            }
            else
            {
                entity.GrossWeightKg = null;
                entity.VolumeCbm = null;
                entity.ChargeableWeightKg = null;
                entity.NumberOfPackages = null;
            }
        }
        else
        {
            var reqTypeForVol = await typeRepository.GetByIdAsync(entity.RequestTypeId, cancellationToken);
            var isSeaOrRoadOrRail = reqTypeForVol != null && (string.Equals(reqTypeForVol.Mode, "Sea", StringComparison.OrdinalIgnoreCase) || string.Equals(reqTypeForVol.Mode, "Road", StringComparison.OrdinalIgnoreCase) || string.Equals(reqTypeForVol.Mode, "Rail", StringComparison.OrdinalIgnoreCase));
            if (d.GrossWeightKg != null) entity.GrossWeightKg = d.GrossWeightKg;
            if (d.VolumeCbm != null)
            {
                entity.VolumeCbm = d.VolumeCbm;
                entity.ChargeableWeightKg = isSeaOrRoadOrRail
                    ? SeaFreightCalculationService.RoundChargeableWeight(SeaFreightCalculationService.CalculateVolumetricWeightMt(d.VolumeCbm.Value))
                    : ExportAirRequestCalculationService.CalculateChargeableFromManualVolume(d.VolumeCbm);
            }
            if (d.ChargeableWeightKg != null) entity.ChargeableWeightKg = d.ChargeableWeightKg;
            if (d.NumberOfPackages != null) entity.NumberOfPackages = d.NumberOfPackages;
        }

        if (d.VasItems != null)
        {
            var reqTypeForVas = await typeRepository.GetByIdAsync(entity.RequestTypeId, cancellationToken);
            var supportsVas = reqTypeForVas != null && ((string.Equals(reqTypeForVas.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase)
                    && (string.Equals(reqTypeForVas.Mode, "Sea", StringComparison.OrdinalIgnoreCase) || string.Equals(reqTypeForVas.Mode, "Road", StringComparison.OrdinalIgnoreCase)))
                || (string.Equals(reqTypeForVas.SubType, "OOG", StringComparison.OrdinalIgnoreCase) && string.Equals(reqTypeForVas.Mode, "Road", StringComparison.OrdinalIgnoreCase))
                || (string.Equals(reqTypeForVas.SubType, "FCL", StringComparison.OrdinalIgnoreCase) && (string.Equals(reqTypeForVas.Mode, "Sea", StringComparison.OrdinalIgnoreCase) || string.Equals(reqTypeForVas.Mode, "Rail", StringComparison.OrdinalIgnoreCase)))
                || (string.Equals(reqTypeForVas.SubType, "LCL", StringComparison.OrdinalIgnoreCase) && string.Equals(reqTypeForVas.Mode, "Sea", StringComparison.OrdinalIgnoreCase)));

            await vasRepository.DeleteByRequestIdAsync(entity.Id, cancellationToken);
            if (supportsVas && d.VasItems.Count > 0)
            {
                var vasItems = d.VasItems.Where(x => x.VasId != Guid.Empty).Select(x => new RequestVas
                {
                    Id = Guid.NewGuid(),
                    RequestId = entity.Id,
                    VasId = x.VasId,
                    VasName = x.VasName,
                    ExecutionPlace = x.ExecutionPlace,
                    Quantity = x.Quantity,
                    Uom = x.Uom,
                    CurrencyId = x.CurrencyId,
                    CurrencyCode = x.CurrencyCode,
                    Total = x.Total,
                    Notes = x.Notes
                }).ToList();
                if (vasItems.Count > 0)
                    await vasRepository.AddRangeAsync(vasItems, cancellationToken);
            }
        }

        await repository.UpdateAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var reqType = await typeRepository.GetByIdAsync(entity.RequestTypeId, cancellationToken);
        var dimsLoaded = await dimensionRepository.GetByRequestIdAsync(entity.Id, cancellationToken);
        var vasLoaded = await vasRepository.GetByRequestIdAsync(entity.Id, cancellationToken);
        var dto = RequestMapper.MapToDto(loaded ?? entity, reqType, dimsLoaded, vasLoaded);

        await actionLogClient.LogAsync("Request changed", $"request number: {entity.RequestNumber} • id: {entity.Id}", entity.ManagerId, entity.ManagerName, cancellationToken);

        return dto;
    }
}

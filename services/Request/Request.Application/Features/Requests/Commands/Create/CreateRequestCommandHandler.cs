using MediatR;
using Request.Application.DTOs.Request;
using Request.Application.Features.Requests;
using Request.Application.Services;
using Request.Application.Validation;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Features.Requests.Commands.Create;

public sealed class CreateRequestCommandHandler(
    IRequestRepository repository,
    IRequestTypeRepository typeRepository,
    IRequestDimensionRepository dimensionRepository,
    IRequestVasRepository vasRepository,
    IActionLogClient actionLogClient) : IRequestHandler<CreateRequestCommand, RequestDto>
{
    public async Task<RequestDto> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        var d = request.Dto;
        var reqType = await typeRepository.GetByIdAsync(d.RequestTypeId, cancellationToken);
        RequestDimensionValidator.Validate(d.Dimensions, reqType);
        var supportsTransitPort = reqType != null && string.Equals(reqType.Direction, "Transit", StringComparison.OrdinalIgnoreCase)
            && (string.Equals(reqType.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase)
                || (string.Equals(reqType.Mode, "Road", StringComparison.OrdinalIgnoreCase) && (string.Equals(reqType.SubType, "FTL", StringComparison.OrdinalIgnoreCase) || string.Equals(reqType.SubType, "LTL", StringComparison.OrdinalIgnoreCase) || string.Equals(reqType.SubType, "OOG", StringComparison.OrdinalIgnoreCase)))
                || (string.Equals(reqType.Mode, "Rail", StringComparison.OrdinalIgnoreCase) && (string.Equals(reqType.SubType, "FCL", StringComparison.OrdinalIgnoreCase) || string.Equals(reqType.SubType, "LCL", StringComparison.OrdinalIgnoreCase) || string.Equals(reqType.SubType, "OOG", StringComparison.OrdinalIgnoreCase))));
        var entity = new RequestEntity
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            RequestNumber = d.RequestNumber,
            RequestTypeId = d.RequestTypeId,
            CompanyId = d.CompanyId, CompanyName = d.CompanyName,
            ManagerId = d.ManagerId, ManagerName = d.ManagerName,
            LogisticianId = d.LogisticianId, LogisticianName = d.LogisticianName,
            DepartmentId = d.DepartmentId, DepartmentName = d.DepartmentName,
            ShipperId = d.ShipperId, ShipperName = d.ShipperName,
            ConsigneeId = d.ConsigneeId, ConsigneeName = d.ConsigneeName,
            MyCustomerTypeId = d.MyCustomerTypeId, MyCustomerTypeName = d.MyCustomerTypeName,
            DispatchDateFrom = d.DispatchDateFrom, DispatchDateTo = d.DispatchDateTo,
            UnloadingDateFrom = d.UnloadingDateFrom, UnloadingDateTo = d.UnloadingDateTo,
            QuotationSent = d.QuotationSent, StatusName = d.StatusName,
            ExtremelyUrgent = d.ExtremelyUrgent, ToAnswerUntilDate = d.ToAnswerUntilDate,
            PriceStandard = d.PriceStandard, CurrencyId = d.CurrencyId, CurrencyCode = d.CurrencyCode,
            PriceWithVat = d.PriceWithVat, VatRate = d.VatRate,
            SourceOfRequestId = d.SourceOfRequestId, SourceOfRequestName = d.SourceOfRequestName,
            RequestPurposeId = d.RequestPurposeId, RequestPurposeName = d.RequestPurposeName,
            GatewayTerminalId = d.GatewayTerminalId, GatewayName = d.GatewayName,
            ViaPortTerminalId = d.ViaPortTerminalId, ViaPortName = d.ViaPortName,
            DestinationTerminalId = d.DestinationTerminalId, DestinationName = d.DestinationName,
            ViaPort2TerminalId = d.ViaPort2TerminalId, ViaPort2Name = d.ViaPort2Name,
            TransitPortTerminalId = supportsTransitPort ? d.TransitPortTerminalId : null,
            TransitPortName = supportsTransitPort ? d.TransitPortName : null,
            CarrierId = d.CarrierId, CarrierName = d.CarrierName,
            StationOfDeliveryTerminalId = d.StationOfDeliveryTerminalId, StationOfDeliveryName = d.StationOfDeliveryName,
            GrossWeightKg = d.GrossWeightKg, VolumeCbm = d.VolumeCbm, ChargeableWeightKg = d.ChargeableWeightKg, NumberOfPackages = d.NumberOfPackages,
            DangerousGoods = d.DangerousGoods, DescriptionOfGoods = d.DescriptionOfGoods, Notes = d.Notes,
            IsActive = true
        };

        if (d.Dimensions is { Count: > 0 })
        {
            var inputs = d.Dimensions.Select(x => new ExportAirRequestCalculationService.DimensionInput(x.Length, x.Width, x.Height, x.Quantity, x.WeightKg)).ToList();
            var isSea = reqType != null && string.Equals(reqType.Mode, "Sea", StringComparison.OrdinalIgnoreCase);
            var isRoad = reqType != null && string.Equals(reqType.Mode, "Road", StringComparison.OrdinalIgnoreCase);
            var isRail = reqType != null && string.Equals(reqType.Mode, "Rail", StringComparison.OrdinalIgnoreCase);
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
        }
        else if (d.VolumeCbm.HasValue && d.VolumeCbm > 0)
        {
            var isSea = reqType != null && string.Equals(reqType.Mode, "Sea", StringComparison.OrdinalIgnoreCase);
            var isRoad = reqType != null && string.Equals(reqType.Mode, "Road", StringComparison.OrdinalIgnoreCase);
            var isRail = reqType != null && string.Equals(reqType.Mode, "Rail", StringComparison.OrdinalIgnoreCase);
            entity.ChargeableWeightKg = (isSea || isRoad || isRail)
                ? SeaFreightCalculationService.RoundChargeableWeight(SeaFreightCalculationService.CalculateVolumetricWeightMt(d.VolumeCbm.Value))
                : ExportAirRequestCalculationService.CalculateChargeableFromManualVolume(d.VolumeCbm);
        }

        await repository.AddAsync(entity, cancellationToken);

        if (d.Dimensions is { Count: > 0 })
        {
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

        var supportsVas = reqType != null && ((string.Equals(reqType.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase)
                && (string.Equals(reqType.Mode, "Sea", StringComparison.OrdinalIgnoreCase) || string.Equals(reqType.Mode, "Road", StringComparison.OrdinalIgnoreCase)))
            || (string.Equals(reqType.SubType, "OOG", StringComparison.OrdinalIgnoreCase) && string.Equals(reqType.Mode, "Road", StringComparison.OrdinalIgnoreCase))
            || (string.Equals(reqType.SubType, "FCL", StringComparison.OrdinalIgnoreCase) && (string.Equals(reqType.Mode, "Sea", StringComparison.OrdinalIgnoreCase) || string.Equals(reqType.Mode, "Rail", StringComparison.OrdinalIgnoreCase)))
            || (string.Equals(reqType.SubType, "LCL", StringComparison.OrdinalIgnoreCase) && string.Equals(reqType.Mode, "Sea", StringComparison.OrdinalIgnoreCase)));

        if (supportsVas && d.VasItems is { Count: > 0 })
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

        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var reqTypeForDto = reqType ?? await typeRepository.GetByIdAsync(entity.RequestTypeId, cancellationToken);
        var dimsLoaded = await dimensionRepository.GetByRequestIdAsync(entity.Id, cancellationToken);
        var vasLoaded = await vasRepository.GetByRequestIdAsync(entity.Id, cancellationToken);
        var dto = RequestMapper.MapToDto(loaded ?? entity, reqTypeForDto, dimsLoaded, vasLoaded);

        await actionLogClient.LogAsync("Request created", $"request_id: {entity.RequestNumber} • id: {entity.Id}", entity.ManagerId, entity.ManagerName, cancellationToken);

        return dto;
    }
}

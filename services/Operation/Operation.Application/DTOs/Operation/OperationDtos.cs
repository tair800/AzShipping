namespace Operation.Application.DTOs.Operation;

public record OperationTypeDto(
    Guid Id,
    string Code,
    string Name,
    string Direction,
    string Mode,
    string? SubType,
    string OperationNumberPrefix,
    string CarrierApiPath,
    string CarrierLabel,
    int SortOrder,
    bool IsActive);

public record OperationDimensionDto(
    Guid Id,
    decimal Length,
    decimal Width,
    decimal Height,
    int Quantity,
    decimal? WeightKg,
    decimal? VolumeCbm,
    string? PackageType);

public record OperationPackageLineDto(Guid Id, int Quantity, string? PackageType, int SortOrder);

public record OperationVasDto(
    Guid Id,
    Guid VasId,
    string? VasName,
    string? ExecutionPlace,
    decimal Quantity,
    string? Uom,
    Guid? CurrencyId,
    string? CurrencyCode,
    decimal? Total,
    string? Notes);

public record OperationDto(
    Guid Id,
    DateTime CreationDate,
    string OperationNumber,
    Guid OperationTypeId,
    string? OperationTypeCode,
    string? OperationTypeName,
    string ModalType,
    string PricingMode,
    string? ClientOrderNumber,
    Guid? CompanyId,
    string? CompanyName,
    Guid? ManagerId,
    string? ManagerName,
    Guid? LogisticianId,
    string? LogisticianName,
    Guid? DepartmentId,
    string? DepartmentName,
    Guid? ShipperId,
    string? ShipperName,
    Guid? ConsigneeId,
    string? ConsigneeName,
    string? MyCustomerParty,
    Guid? MyCustomerClientId,
    string? MyCustomerName,
    DateTime? StartDate,
    string? StartTime,
    Guid? IncotermId,
    string? IncotermName,
    string? FreightPrepaidCollect,
    string? MoveTypeName,
    string? OtherPrepaidCollect,
    Guid? SalesmanId,
    string? SalesmanName,
    decimal? PriceStandard,
    Guid? CurrencyId,
    string? CurrencyCode,
    decimal? PriceWithVat,
    string? VatRate,
    Guid? DeferredPaymentConditionId,
    string? DeferredPaymentConditionName,
    int? DeferredPaymentDays,
    bool IncludePickup,
    Guid? PickupCountryId,
    string? PickupCountryName,
    Guid? PickupStateId,
    string? PickupStateName,
    Guid? PickupCityId,
    string? PickupCityName,
    string? PickupZipCode,
    Guid? GatewayTerminalId,
    string? GatewayName,
    Guid? ViaPortTerminalId,
    string? ViaPortName,
    Guid? DestinationTerminalId,
    string? DestinationName,
    Guid? ViaPort2TerminalId,
    string? ViaPort2Name,
    Guid? CarrierId,
    string? CarrierName,
    string? FlightNumber,
    string? Mawb,
    bool IncludeDelivery,
    Guid? DeliveryCountryId,
    string? DeliveryCountryName,
    Guid? DeliveryStateId,
    string? DeliveryStateName,
    Guid? DeliveryCityId,
    string? DeliveryCityName,
    string? DeliveryZipCode,
    decimal? GrossWeightKg,
    decimal? VolumeCbm,
    decimal? ChargeableWeightKg,
    int? NumberOfPackages,
    bool DangerousGoods,
    string? DescriptionOfGoods,
    Guid? AgentId,
    string? AgentName,
    string? Reference1,
    string? Reference2,
    string? MainHarmonize,
    string? NotesToBePrinted,
    string? TrackingNumber,
    Guid? TemplateId,
    string? TemplateName,
    string? Notes,
    bool IsActive,
    string? OperationStageName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CargoName,
    Guid? LoadingMethodId,
    string? LoadingMethodName,
    Guid? CargoTransportTypeId,
    string? CargoTransportTypeName,
    decimal? ConsignmentPrice,
    Guid? ConsignmentCurrencyId,
    string? ConsignmentCurrencyCode,
    string? CargoAdditionalInformation,
    Guid? PortOfDeliveryTerminalId,
    string? PortOfDeliveryName,
    string? OceanBillOfLading,
    Guid? VesselId,
    string? VesselName,
    string? RoadTruckerNumber,
    string? RoadWaybillNumber,
    bool IncludeVas,
    IReadOnlyList<OperationVasDto> VasItems,
    IReadOnlyList<OperationPackageLineDto> PackageLines,
    IReadOnlyList<OperationDimensionDto> Dimensions);

/// <summary>VAS line preview on operations list (additional expenses column).</summary>
public record OperationListExpensePreviewDto(string? Description, decimal? Amount, string? CurrencyCode);

/// <summary>
/// Slim row for the operations grid (Figma list). Freight prefers price standard, then consignment price;
/// VAS totals are summed when line currency matches the operation currency. HasDocuments is false until a documents API exists.
/// </summary>
public record OperationListItemDto(
    Guid Id,
    DateTime CreationDate,
    string OperationNumber,
    string StatusDisplayName,
    string? OperationTypeCode,
    string? OperationTypeName,
    string? Direction,
    string? Mode,
    string? SubType,
    string? ModalType,
    string? ClientDisplayName,
    string? CarrierName,
    string? RouteSummary,
    string? TripReferenceSummary,
    string? CargoSummary,
    decimal? VolumeCbm,
    decimal? GrossWeightKg,
    string? PrimaryDimensionsLabel,
    decimal? FreightAmount,
    string? FreightCurrencyCode,
    IReadOnlyList<OperationListExpensePreviewDto> VasExpenseLines,
    decimal VasExpenseTotalSameCurrency,
    string? VasExpenseCurrencyCode,
    decimal? ProfitApproxSameCurrency,
    bool HasDocuments);

/// <summary>
/// One row per operation for the Trips and Cargos list UIs (Figma). Until dedicated trip/cargo leg entities exist, this denormalizes
/// the operation (main carriage + pickup/delivery + cargo scalars) into grid columns. Returned by <c>/api/operations/trips-list</c> and <c>/api/operations/cargos-list</c>.
/// </summary>
public record TripListItemDto(
    Guid OperationId,
    DateTime CreationDate,
    string OperationNumber,
    string? Direction,
    string? Mode,
    string? SubType,
    string? ModalType,
    string? CompanyName,
    string? ClientDisplayName,
    DateTime? LoadingDate,
    DateTime? UnloadingDate,
    string? SenderName,
    string? LoadingPlace,
    string? ReceiverName,
    string? UnloadingPlace,
    string StatusDisplayName,
    string? PlaceNote,
    DateTime? PlannedDate,
    string? PlannedTime,
    string? Comments,
    string? CargoNumber,
    string? CargoName,
    string? CargoParametersSummary,
    string? Attributes,
    string? CarrierName,
    string? TripReference);

/// <summary>Payload for create/update — mirrors UI fields; lookups use id + denormalized name from Settings/Clients/Carrier.</summary>
public class SaveOperationDto
{
    public string? OperationNumber { get; init; }
    public Guid? OperationTypeId { get; init; }
    public string? ModalType { get; init; }
    public string? PricingMode { get; init; }
    public string? ClientOrderNumber { get; init; }
    public Guid? CompanyId { get; init; }
    public string? CompanyName { get; init; }
    public Guid? ManagerId { get; init; }
    public string? ManagerName { get; init; }
    public Guid? LogisticianId { get; init; }
    public string? LogisticianName { get; init; }
    public Guid? DepartmentId { get; init; }
    public string? DepartmentName { get; init; }
    public Guid? ShipperId { get; init; }
    public string? ShipperName { get; init; }
    public Guid? ConsigneeId { get; init; }
    public string? ConsigneeName { get; init; }
    public string? MyCustomerParty { get; init; }
    public Guid? MyCustomerClientId { get; init; }
    public string? MyCustomerName { get; init; }
    public DateTime? StartDate { get; init; }
    public string? StartTime { get; init; }
    public Guid? IncotermId { get; init; }
    public string? IncotermName { get; init; }
    public string? FreightPrepaidCollect { get; init; }
    public string? MoveTypeName { get; init; }
    public string? OtherPrepaidCollect { get; init; }
    public Guid? SalesmanId { get; init; }
    public string? SalesmanName { get; init; }
    public decimal? PriceStandard { get; init; }
    public Guid? CurrencyId { get; init; }
    public string? CurrencyCode { get; init; }
    public decimal? PriceWithVat { get; init; }
    public string? VatRate { get; init; }
    public Guid? DeferredPaymentConditionId { get; init; }
    public string? DeferredPaymentConditionName { get; init; }
    public int? DeferredPaymentDays { get; init; }
    public bool? IncludePickup { get; init; }
    public Guid? PickupCountryId { get; init; }
    public string? PickupCountryName { get; init; }
    public Guid? PickupStateId { get; init; }
    public string? PickupStateName { get; init; }
    public Guid? PickupCityId { get; init; }
    public string? PickupCityName { get; init; }
    public string? PickupZipCode { get; init; }
    public Guid? GatewayTerminalId { get; init; }
    public string? GatewayName { get; init; }
    public Guid? ViaPortTerminalId { get; init; }
    public string? ViaPortName { get; init; }
    public Guid? DestinationTerminalId { get; init; }
    public string? DestinationName { get; init; }
    public Guid? ViaPort2TerminalId { get; init; }
    public string? ViaPort2Name { get; init; }
    public Guid? CarrierId { get; init; }
    public string? CarrierName { get; init; }
    public string? FlightNumber { get; init; }
    public string? Mawb { get; init; }
    public bool? IncludeDelivery { get; init; }
    public Guid? DeliveryCountryId { get; init; }
    public string? DeliveryCountryName { get; init; }
    public Guid? DeliveryStateId { get; init; }
    public string? DeliveryStateName { get; init; }
    public Guid? DeliveryCityId { get; init; }
    public string? DeliveryCityName { get; init; }
    public string? DeliveryZipCode { get; init; }
    public decimal? GrossWeightKg { get; init; }
    public decimal? VolumeCbm { get; init; }
    public decimal? ChargeableWeightKg { get; init; }
    public int? NumberOfPackages { get; init; }
    public bool? DangerousGoods { get; init; }
    public string? DescriptionOfGoods { get; init; }
    public Guid? AgentId { get; init; }
    public string? AgentName { get; init; }
    public string? Reference1 { get; init; }
    public string? Reference2 { get; init; }
    public string? MainHarmonize { get; init; }
    public string? NotesToBePrinted { get; init; }
    public string? TrackingNumber { get; init; }
    public Guid? TemplateId { get; init; }
    public string? TemplateName { get; init; }
    public string? Notes { get; init; }
    public bool? IsActive { get; init; }
    public string? OperationStageName { get; init; }
    public string? CargoName { get; init; }
    public Guid? LoadingMethodId { get; init; }
    public string? LoadingMethodName { get; init; }
    public Guid? CargoTransportTypeId { get; init; }
    public string? CargoTransportTypeName { get; init; }
    public decimal? ConsignmentPrice { get; init; }
    public Guid? ConsignmentCurrencyId { get; init; }
    public string? ConsignmentCurrencyCode { get; init; }
    public string? CargoAdditionalInformation { get; init; }
    public Guid? PortOfDeliveryTerminalId { get; init; }
    public string? PortOfDeliveryName { get; init; }
    public string? OceanBillOfLading { get; init; }
    public Guid? VesselId { get; init; }
    public string? VesselName { get; init; }
    public string? RoadTruckerNumber { get; init; }
    public string? RoadWaybillNumber { get; init; }
    public bool? IncludeVas { get; init; }
    public IReadOnlyList<SaveOperationVasDto>? VasItems { get; init; }
    public IReadOnlyList<SaveOperationPackageLineDto>? PackageLines { get; init; }
    public IReadOnlyList<SaveOperationDimensionDto>? Dimensions { get; init; }
}

public record SaveOperationVasDto(
    Guid VasId,
    string? VasName,
    string? ExecutionPlace,
    decimal Quantity,
    string? Uom,
    Guid? CurrencyId,
    string? CurrencyCode,
    decimal? Total,
    string? Notes);

public record SaveOperationPackageLineDto(int Quantity, string? PackageType);

public record SaveOperationDimensionDto(decimal Length, decimal Width, decimal Height, int Quantity, decimal? WeightKg, decimal? VolumeCbm, string? PackageType);

public record CalculateAirDimensionsRequest(
    IReadOnlyList<CalculateAirDimensionRowDto>? Dimensions,
    decimal? VolumeCbm,
    bool UseSeaLclVolumetricFactor = false);
public record CalculateAirDimensionRowDto(decimal Length, decimal Width, decimal Height, int Quantity, decimal? WeightKg, decimal? VolumeCbm);
public record CalculateAirDimensionsResponse(decimal? GrossWeightKg, decimal? VolumeCbm, decimal? ChargeableWeightKg, int? NumberOfPackages, decimal? VolumetricWeightKg);

/// <summary>One side of an operation finance line (income or expense).</summary>
public record FinanceLineInputDto(decimal? Amount, decimal? UnitPrice, decimal? VatRatePercent);

public record CalculateFinanceAmountsRequest(FinanceLineInputDto? Income, FinanceLineInputDto? Expense);

public record FinanceLineCalculationDto(decimal LineSubtotal, decimal VatAmount, decimal TotalWithVat);

/// <summary>Server-calculated line totals and optional profit when both income and expense are supplied.</summary>
public record CalculateFinanceAmountsResponse(
    FinanceLineCalculationDto? Income,
    FinanceLineCalculationDto? Expense,
    decimal? ProfitExVat,
    decimal? ProfitInclVat);

namespace Request.Domain.AggregatesModel.RequestAggregate;

/// <summary>
/// Generic request - works for Import Air, Export Train, Transit Sea, etc.
/// RequestType defines the specifics (prefix, carrier API, labels).
/// </summary>
public class RequestEntity
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string RequestNumber { get; set; } = string.Empty;
    public Guid RequestTypeId { get; set; }

    // Company and user
    public Guid? CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public Guid? ManagerId { get; set; }
    public string? ManagerName { get; set; }
    public Guid? LogisticianId { get; set; }
    public string? LogisticianName { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }

    // Parties
    public Guid? ShipperId { get; set; }
    public string? ShipperName { get; set; }
    public Guid? ConsigneeId { get; set; }
    public string? ConsigneeName { get; set; }
    public Guid? MyCustomerTypeId { get; set; }
    public string? MyCustomerTypeName { get; set; }

    // Expected delivery
    public DateTime? DispatchDateFrom { get; set; }
    public DateTime? DispatchDateTo { get; set; }
    public DateTime? UnloadingDateFrom { get; set; }
    public DateTime? UnloadingDateTo { get; set; }
    public bool? QuotationSent { get; set; }
    public string? StatusName { get; set; }
    public bool ExtremelyUrgent { get; set; }
    public DateTime? ToAnswerUntilDate { get; set; }

    // Price
    public decimal? PriceStandard { get; set; }
    public Guid? CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal? PriceWithVat { get; set; }
    public string? VatRate { get; set; }

    // Source and main carriage
    public Guid? SourceOfRequestId { get; set; }
    public string? SourceOfRequestName { get; set; }
    public Guid? RequestPurposeId { get; set; }
    public string? RequestPurposeName { get; set; }
    public Guid? GatewayTerminalId { get; set; }
    public string? GatewayName { get; set; }
    public Guid? ViaPortTerminalId { get; set; }
    public string? ViaPortName { get; set; }
    public Guid? DestinationTerminalId { get; set; }
    public string? DestinationName { get; set; }
    public Guid? ViaPort2TerminalId { get; set; }
    public string? ViaPort2Name { get; set; }
    public Guid? TransitPortTerminalId { get; set; }
    public string? TransitPortName { get; set; }
    // Generic carrier - Airline / Train operator / Shipping line depending on RequestType
    public Guid? CarrierId { get; set; }
    public string? CarrierName { get; set; }
    /// <summary>For Rail: final delivery station.</summary>
    public Guid? StationOfDeliveryTerminalId { get; set; }
    public string? StationOfDeliveryName { get; set; }

    // Order details
    public decimal? GrossWeightKg { get; set; }
    public decimal? VolumeCbm { get; set; }
    public decimal? ChargeableWeightKg { get; set; }
    public int? NumberOfPackages { get; set; }
    public bool DangerousGoods { get; set; }
    public string? DescriptionOfGoods { get; set; }

    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

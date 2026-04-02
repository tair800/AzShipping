using Quotes.Domain.AggregatesModel.AddressAggregate;

namespace Quotes.Domain.AggregatesModel.QuoteAggregate;

/// <summary>
/// Quote entity - works for Export Air, Import Sea, etc. QuoteType defines specifics.
/// </summary>
public class QuoteEntity
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string QuoteNumber { get; set; } = string.Empty;
    public Guid QuoteTypeId { get; set; }

    // Company and user
    public Guid? CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public Guid? ManagerId { get; set; }
    public string? ManagerName { get; set; }
    public Guid? LogisticianId { get; set; }
    public string? LogisticianName { get; set; }
    public Guid? HandlerId { get; set; }
    public string? HandlerName { get; set; }
    public Guid? AccountManagerId { get; set; }
    public string? AccountManagerName { get; set; }
    public Guid? OpenedById { get; set; }
    public string? OpenedByName { get; set; }
    /// <summary>Identity <c>User.Id</c> (<c>long</c>). Preferred over <see cref="ManagerId"/> when set.</summary>
    public long? ManagerUserId { get; set; }
    public long? HandlerUserId { get; set; }
    public long? AccountManagerUserId { get; set; }
    public long? OpenedByUserId { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public string? QuoteStatus { get; set; }

    // Parties
    public Guid? ShipperId { get; set; }
    public string? ShipperName { get; set; }
    public Guid? ConsigneeId { get; set; }
    public string? ConsigneeName { get; set; }
    public Guid? MyCustomerTypeId { get; set; }
    public string? MyCustomerTypeName { get; set; }

    // Quote-specific: General
    public string? RateType { get; set; } // Spot Rate, Routing Rate
    public DateTime? StartDate { get; set; }
    public DateTime? Etd { get; set; }
    public DateTime? Eta { get; set; }
    public Guid? IncotermId { get; set; }
    public string? IncotermName { get; set; }
    public int? ExpirationDays { get; set; }
    public int? PurchaseFreeDays { get; set; }
    public int? SaleFreeDays { get; set; }
    public Guid? MoveTypeId { get; set; }
    public string? MoveTypeName { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool? CloseAutomaticallyDeclined { get; set; }
    public int? CloseAutomaticallyDeclinedDays { get; set; }
    public DateTime? AutomaticallyCloseDate { get; set; }
    public bool IncludeInsurance { get; set; }
    public decimal? InsuranceValue { get; set; }
    public bool IsStackable { get; set; }
    public bool IncludeImportDutyCharges { get; set; }
    public int? TransitTime { get; set; }
    public bool IsFreighter { get; set; }
    public int? DepartureFrequency { get; set; }
    public decimal? ValueOfGoods { get; set; }

    // Price
    public decimal? PriceStandard { get; set; }
    public string? RmbVwt { get; set; }
    public Guid? CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal? PriceWithVat { get; set; }
    public string? MinVat { get; set; }
    public string? VatRate { get; set; }
    public string? VatNote { get; set; }

    // Pickup
    public bool IncludePickup { get; set; }
    public Guid? PickupAddressId { get; set; }
    public Address? PickupAddress { get; set; }
    public Guid? PickupCountryId { get; set; }
    public string? PickupCountryName { get; set; }
    public Guid? PickupStateId { get; set; }
    public string? PickupStateName { get; set; }
    public Guid? PickupCityId { get; set; }
    public string? PickupCityName { get; set; }
    public string? PickupZipCode { get; set; }

    // Main carriage
    public Guid? GatewayTerminalId { get; set; }
    public string? GatewayName { get; set; }
    public Guid? ViaPortTerminalId { get; set; }
    public string? ViaPortName { get; set; }
    public Guid? DestinationTerminalId { get; set; }
    public string? DestinationName { get; set; }
    public Guid? ViaPort2TerminalId { get; set; }
    public string? ViaPort2Name { get; set; }
    public Guid? CarrierId { get; set; }
    public string? CarrierName { get; set; }
    public Guid? MyPortTerminalId { get; set; }
    public string? MyPortName { get; set; }
    public Guid? MyPort2TerminalId { get; set; }
    public string? MyPort2Name { get; set; }
    public string? PortOfDeliveryName { get; set; }

    // VAS (Value-Added Services) – relation to General.VAS
    public Guid? VasId { get; set; }
    public bool IncludeVas { get; set; }
    public string? VasServiceName { get; set; }
    public string? ExecutionPlace { get; set; }
    public decimal? VasQuantity { get; set; }
    public string? VasUom { get; set; }
    public string? VasCurrencyCode { get; set; }
    public decimal? VasTotal { get; set; }
    public string? VasNotes { get; set; }

    // Delivery
    public bool IncludeDelivery { get; set; }
    public Guid? DeliveryAddressId { get; set; }
    public Address? DeliveryAddress { get; set; }
    public Guid? DeliveryCountryId { get; set; }
    public string? DeliveryCountryName { get; set; }
    public Guid? DeliveryStateId { get; set; }
    public string? DeliveryStateName { get; set; }
    public Guid? DeliveryCityId { get; set; }
    public string? DeliveryCityName { get; set; }
    public string? DeliveryZipCode { get; set; }

    // Order details
    public decimal? GrossWeightKg { get; set; }
    public decimal? VolumeCbm { get; set; }
    public decimal? ChargeableWeightKg { get; set; }
    public int? NumberOfPackages { get; set; }
    public bool DangerousGoods { get; set; }
    public string? DescriptionOfGoods { get; set; }
    public decimal? Quantity1 { get; set; }
    public decimal? Quantity2 { get; set; }
    public decimal? Quantity3 { get; set; }
    public decimal? Quantity4 { get; set; }
    public string? PackageType1 { get; set; }
    public string? PackageType2 { get; set; }
    public string? PackageType3 { get; set; }
    public string? PackageType4 { get; set; }
    public decimal? Quantity5 { get; set; }
    public string? PackageType5 { get; set; }

    // Additional
    public string? ShipperRef2 { get; set; }
    public string? ConsigneeRef2 { get; set; }
    public Guid? AgentId { get; set; }
    public string? AgentName { get; set; }
    public string? NotesToBePrinted { get; set; }
    public string? Notes { get; set; }

    // Lifecycle / actions
    public DateTime? SentToCustomerAt { get; set; }
    public bool IsCancelled { get; set; }
    public DateTime? CancelledAt { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

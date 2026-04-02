namespace Operation.Domain.AggregatesModel.OperationAggregate;

/// <summary>Air / Sea / Road / Rail — terminals map to gateway/via/destination; Sea &amp; Rail FCL/breakbulk and Road FTL use package lines; Sea LCL &amp; Rail LCL use dimensions (1000 kg/CBM).</summary>
public class LogisticsOperation
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string OperationNumber { get; set; } = string.Empty;
    public Guid OperationTypeId { get; set; }

    public string ModalType { get; set; } = "Unimodal";
    public string PricingMode { get; set; } = "RoutingRates";
    public string? ClientOrderNumber { get; set; }

    public Guid? CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public Guid? ManagerId { get; set; }
    public string? ManagerName { get; set; }
    public Guid? LogisticianId { get; set; }
    public string? LogisticianName { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }

    public Guid? ShipperId { get; set; }
    public string? ShipperName { get; set; }
    public Guid? ConsigneeId { get; set; }
    public string? ConsigneeName { get; set; }

    public string? MyCustomerParty { get; set; }
    public Guid? MyCustomerClientId { get; set; }
    public string? MyCustomerName { get; set; }

    public DateTime? StartDate { get; set; }
    public string? StartTime { get; set; }

    public Guid? IncotermId { get; set; }
    public string? IncotermName { get; set; }
    public string? FreightPrepaidCollect { get; set; }
    public string? MoveTypeName { get; set; }
    public string? OtherPrepaidCollect { get; set; }
    public Guid? SalesmanId { get; set; }
    public string? SalesmanName { get; set; }

    public decimal? PriceStandard { get; set; }
    public Guid? CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal? PriceWithVat { get; set; }
    public string? VatRate { get; set; }
    public Guid? DeferredPaymentConditionId { get; set; }
    public string? DeferredPaymentConditionName { get; set; }
    public int? DeferredPaymentDays { get; set; }

    public bool IncludePickup { get; set; }
    public Guid? PickupCountryId { get; set; }
    public string? PickupCountryName { get; set; }
    public Guid? PickupStateId { get; set; }
    public string? PickupStateName { get; set; }
    public Guid? PickupCityId { get; set; }
    public string? PickupCityName { get; set; }
    public string? PickupZipCode { get; set; }

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
    public string? FlightNumber { get; set; }
    public string? Mawb { get; set; }

    /// <summary>Sea FCL — delivery terminal (often CY).</summary>
    public Guid? PortOfDeliveryTerminalId { get; set; }
    public string? PortOfDeliveryName { get; set; }
    public string? OceanBillOfLading { get; set; }
    public Guid? VesselId { get; set; }
    public string? VesselName { get; set; }

    /// <summary>Road — truck / trailer or vehicle reference (free text).</summary>
    public string? RoadTruckerNumber { get; set; }
    /// <summary>Road — CMR / RWB / HWB / B/L number.</summary>
    public string? RoadWaybillNumber { get; set; }

    public bool IncludeDelivery { get; set; }
    public Guid? DeliveryCountryId { get; set; }
    public string? DeliveryCountryName { get; set; }
    public Guid? DeliveryStateId { get; set; }
    public string? DeliveryStateName { get; set; }
    public Guid? DeliveryCityId { get; set; }
    public string? DeliveryCityName { get; set; }
    public string? DeliveryZipCode { get; set; }

    public decimal? GrossWeightKg { get; set; }
    public decimal? VolumeCbm { get; set; }
    public decimal? ChargeableWeightKg { get; set; }
    public int? NumberOfPackages { get; set; }
    public bool DangerousGoods { get; set; }
    public string? DescriptionOfGoods { get; set; }

    public Guid? AgentId { get; set; }
    public string? AgentName { get; set; }
    public string? Reference1 { get; set; }
    public string? Reference2 { get; set; }
    public string? MainHarmonize { get; set; }
    public string? NotesToBePrinted { get; set; }
    public string? TrackingNumber { get; set; }

    public Guid? TemplateId { get; set; }
    public string? TemplateName { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;

    /// <summary>Lifecycle stage for list UI (e.g. Planning). Optional — list API defaults to &quot;Planning&quot; when null.</summary>
    public string? OperationStageName { get; set; }

    /// <summary>Fill-dimensions modal (Export Air) — cargo block.</summary>
    public string? CargoName { get; set; }
    public Guid? LoadingMethodId { get; set; }
    public string? LoadingMethodName { get; set; }
    public Guid? CargoTransportTypeId { get; set; }
    public string? CargoTransportTypeName { get; set; }
    public decimal? ConsignmentPrice { get; set; }
    public Guid? ConsignmentCurrencyId { get; set; }
    public string? ConsignmentCurrencyCode { get; set; }
    public string? CargoAdditionalInformation { get; set; }

    /// <summary>Sea / rail breakbulk — value-added service block.</summary>
    public bool IncludeVas { get; set; }

    public ICollection<OperationDimension> Dimensions { get; set; } = new List<OperationDimension>();
    public ICollection<OperationPackageLine> PackageLines { get; set; } = new List<OperationPackageLine>();
    public ICollection<OperationVas> VasItems { get; set; } = new List<OperationVas>();
}

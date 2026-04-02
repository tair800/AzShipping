namespace Request.Domain.AggregatesModel.RequestAggregate;

/// <summary>
/// Defines a request type (Import Air, Export Train, etc.). All configuration in one place.
/// Add or change types here - no code changes needed.
/// </summary>
public class RequestType
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;      // e.g. "import-air", "export-train"
    public string Name { get; set; } = string.Empty;      // e.g. "Import Air", "Export Train"
    public string Direction { get; set; } = string.Empty; // Import, Export, Transit, Domestic
    public string Mode { get; set; } = string.Empty;      // Air, Sea, Road, Rail
    /// <summary>Optional sub-type e.g. for Sea: FCL, LCL, Breakbulk.</summary>
    public string? SubType { get; set; }
    public string RequestNumberPrefix { get; set; } = string.Empty; // e.g. "IMP-AIR-", "EXP-TRN-"
    /// <summary>API path for carrier dropdown: airlines, railwaystations, shippinglines, etc.</summary>
    public string CarrierApiPath { get; set; } = string.Empty;
    /// <summary>Label for carrier field: Airline, Train operator, Shipping line, etc.</summary>
    public string CarrierLabel { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

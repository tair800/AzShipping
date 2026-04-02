namespace Carrier.Domain.AggregatesModel.VehicleAggregate;

/// <summary>
/// Independent vehicle entity. Can optionally relate to Company (from Settings).
/// </summary>
public class Vehicle
{
    public Guid Id { get; set; }
    public string VehicleNumber { get; set; } = string.Empty;   // Manual - required
    public DateTime? DateOfCreation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Guid? CompanyId { get; set; }                       // From settings Companies (optional)
    public Guid? CarrierId { get; set; }                       // Link to carrier

    // Basic - dropdown or manual (combobox)
    public Guid? BrandId { get; set; }              // From settings Vehicle brands
    public string? BrandName { get; set; }          // Manual override when not in list
    public Guid? ModelId { get; set; }              // From settings Vehicle models
    public string? ModelName { get; set; }          // Manual override when not in list
    public Guid? EuroEmissionClassId { get; set; }  // From settings Euro emission classes
    public Guid? TransportTypeId { get; set; }      // From settings Transport types
    public Guid? GroupId { get; set; }              // From settings Vehicle groups

    // Basic - manual
    public string? TrailerNumber { get; set; }
    public string? BodyNumber { get; set; }
    public string? LicenceNumber { get; set; }
    public string? Drivers { get; set; }            // Manual - names
    public string? FuelCard { get; set; }
    public string? TransportInformation { get; set; }

    // Basic - dates (calendar)
    public DateTime? ProductionDate { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public DateTime? TechPassportValidity { get; set; }
    public DateTime? LicenceValidityDate { get; set; }

    // Basic - checkbox
    public bool OwnTransport { get; set; }

    // Specifications - all manual
    public decimal? VehicleFullWeight { get; set; }
    public decimal? VehicleEmptyWeight { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public int? VehicleAxles { get; set; }
    public decimal? MaxWeight { get; set; }
    public int? MaxEuroPallets { get; set; }

    public string? Status { get; set; }             // Optional - could be dropdown from settings later
}

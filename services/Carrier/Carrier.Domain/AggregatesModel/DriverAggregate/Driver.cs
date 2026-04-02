namespace Carrier.Domain.AggregatesModel.DriverAggregate;

/// <summary>
/// Independent driver entity. Can be linked to one or more carriers (DriverCarriers).
/// </summary>
public class Driver
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Manual
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? MiddleName { get; set; }
    public string? Passport { get; set; }
    public string? DrivingLicenceNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BankAccount { get; set; }
    public string? FuelCard { get; set; }
    public string? Notes { get; set; }

    // File paths (uploaded documents)
    public string? PassportFilePath { get; set; }
    public string? DrivingLicenceFilePath { get; set; }

    // From settings - Driving licence categories (store IDs; Settings is another service)
    public ICollection<DriverDrivingLicenceCategory> DrivingLicenceCategories { get; set; } = new List<DriverDrivingLicenceCategory>();

    // Carriers - one or more (many-to-many in same DB)
    public ICollection<DriverCarrier> DriverCarriers { get; set; } = new List<DriverCarrier>();

    public DateTime? DateOfEmployment { get; set; }
    public bool IsDeactive { get; set; }
}

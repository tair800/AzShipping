namespace Carrier.Application.DTOs.Driver;

public class CreateDriverDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? MiddleName { get; set; }
    public string? Passport { get; set; }
    public string? DrivingLicenceNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BankAccount { get; set; }
    public string? FuelCard { get; set; }
    public string? Notes { get; set; }
    public List<Guid> DrivingLicenceCategoryIds { get; set; } = new();
    public List<Guid> CarrierIds { get; set; } = new();
    public DateTime? DateOfEmployment { get; set; }
    public bool IsDeactive { get; set; }
}

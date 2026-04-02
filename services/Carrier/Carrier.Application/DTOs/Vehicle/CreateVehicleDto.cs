namespace Carrier.Application.DTOs.Vehicle;

public class CreateVehicleDto
{
    public string VehicleNumber { get; set; } = string.Empty;
    public DateTime? DateOfCreation { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? CarrierId { get; set; }
    public Guid? BrandId { get; set; }
    public string? BrandName { get; set; }
    public Guid? ModelId { get; set; }
    public string? ModelName { get; set; }
    public Guid? EuroEmissionClassId { get; set; }
    public Guid? TransportTypeId { get; set; }
    public Guid? GroupId { get; set; }
    public string? TrailerNumber { get; set; }
    public string? BodyNumber { get; set; }
    public string? LicenceNumber { get; set; }
    public string? Drivers { get; set; }
    public string? FuelCard { get; set; }
    public string? TransportInformation { get; set; }
    public DateTime? ProductionDate { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public DateTime? TechPassportValidity { get; set; }
    public DateTime? LicenceValidityDate { get; set; }
    public bool OwnTransport { get; set; }
    public decimal? VehicleFullWeight { get; set; }
    public decimal? VehicleEmptyWeight { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public int? VehicleAxles { get; set; }
    public decimal? MaxWeight { get; set; }
    public int? MaxEuroPallets { get; set; }
    public string? Status { get; set; }
}

namespace Carrier.Application.DTOs.CarrierDirection;

public class CarrierDirectionDto
{
    public Guid Id { get; set; }
    public Guid CarrierId { get; set; }
    public Guid? DepartureCountryId { get; set; }
    public Guid? DepartureGlobalZoneId { get; set; }
    public Guid? DepartureCityId { get; set; }
    public Guid? ArrivalCountryId { get; set; }
    public Guid? ArrivalGlobalZoneId { get; set; }
    public Guid? ArrivalCityId { get; set; }
    public string? CarrierLicences { get; set; }
    public string? Comments { get; set; }
    public List<Guid> TransportTypeIds { get; set; } = new();
}

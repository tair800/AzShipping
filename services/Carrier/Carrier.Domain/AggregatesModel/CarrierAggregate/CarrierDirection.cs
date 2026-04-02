namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

/// <summary>
/// Carrier direction/route. Region = Global Zone (from Settings).
/// </summary>
public class CarrierDirection
{
    public Guid Id { get; set; }
    public Guid CarrierId { get; set; }

    // From (Departure)
    public Guid? DepartureCountryId { get; set; }   // Settings Countries
    public Guid? DepartureGlobalZoneId { get; set; } // Settings GlobalZones (Region)
    public Guid? DepartureCityId { get; set; }      // Settings Cities

    // To (Arrival)
    public Guid? ArrivalCountryId { get; set; }
    public Guid? ArrivalGlobalZoneId { get; set; }
    public Guid? ArrivalCityId { get; set; }

    public string? CarrierLicences { get; set; }    // Manual
    public string? Comments { get; set; }

    // Transport types (from Settings; store IDs in junction)
    public ICollection<CarrierDirectionTransportType> TransportTypes { get; set; } = new List<CarrierDirectionTransportType>();

    public Carrier Carrier { get; set; } = null!;
}

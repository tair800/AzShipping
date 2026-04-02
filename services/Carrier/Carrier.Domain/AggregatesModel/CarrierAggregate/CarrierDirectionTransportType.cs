namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

/// <summary>
/// Junction: direction can have multiple transport types (IDs from Settings).
/// </summary>
public class CarrierDirectionTransportType
{
    public Guid CarrierDirectionId { get; set; }
    public Guid TransportTypeId { get; set; }
    public CarrierDirection CarrierDirection { get; set; } = null!;
}

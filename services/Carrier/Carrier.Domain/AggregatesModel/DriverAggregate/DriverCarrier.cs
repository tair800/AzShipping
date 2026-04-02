namespace Carrier.Domain.AggregatesModel.DriverAggregate;

public class DriverCarrier
{
    public Guid DriverId { get; set; }
    public Guid CarrierId { get; set; }
    public Driver Driver { get; set; } = null!;
    public global::Carrier.Domain.AggregatesModel.CarrierAggregate.Carrier Carrier { get; set; } = null!;
}

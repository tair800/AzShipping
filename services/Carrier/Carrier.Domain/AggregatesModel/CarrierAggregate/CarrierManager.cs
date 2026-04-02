namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

public class CarrierManager
{
    public Guid Id { get; set; }
    public Guid CarrierId { get; set; }
    public Carrier Carrier { get; set; } = null!;
    public Guid UserId { get; set; }                   // From Users service
}

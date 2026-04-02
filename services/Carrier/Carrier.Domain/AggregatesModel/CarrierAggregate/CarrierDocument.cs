namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

public class CarrierDocument
{
    public Guid Id { get; set; }
    public Guid CarrierId { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public string DocumentName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? Comments { get; set; }
    public bool AvailableForClient { get; set; }
    public bool IsSent { get; set; }
    public string? FilePath { get; set; }

    public Carrier Carrier { get; set; } = null!;
}

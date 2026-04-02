namespace Clients.Domain.AggregatesModel.NegotiationAggregate;

public class NegotiationResult
{
    public Guid Id { get; set; }
    public Guid NegotiationId { get; set; }
    public string Result { get; set; } = string.Empty;
    public string? Comments { get; set; }
    public DateTime ResultDate { get; set; }
}

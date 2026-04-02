namespace Request.Domain.AggregatesModel.RequestNegotiationAggregate;

public class RequestNegotiation
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string? ClientName { get; set; }
    public Guid? WayOfNegotiationId { get; set; }
    public string? WayOfNegotiationName { get; set; }
    public DateTime CreationDate { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public string? Result { get; set; }
}

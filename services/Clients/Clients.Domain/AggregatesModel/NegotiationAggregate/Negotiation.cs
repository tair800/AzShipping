namespace Clients.Domain.AggregatesModel.NegotiationAggregate;

public class Negotiation
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string PersonName { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public Guid? WayOfNegotiationId { get; set; }
    public string? QuestionsAndAnswers { get; set; }
    public string? Result { get; set; }
}

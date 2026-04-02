namespace Clients.Application.DTOs.Negotiation;

public record NegotiationDto
{
    public Guid Id { get; init; }
    public Guid ClientId { get; init; }
    public string PersonName { get; init; } = string.Empty;
    public DateTime CreationDate { get; init; }
    public Guid? WayOfNegotiationId { get; init; }
    public string? QuestionsAndAnswers { get; init; }
    public string? Result { get; init; }
    public IReadOnlyList<NegotiationResultDto> Results { get; init; } = Array.Empty<NegotiationResultDto>();
}

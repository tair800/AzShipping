namespace Clients.Application.DTOs.Negotiation;

public record CreateNegotiationDto(
    Guid ClientId,
    string PersonName,
    string? CreationDate,  // "yyyy-MM-dd" or ISO 8601 - parsed in handler
    Guid? WayOfNegotiationId,
    string? QuestionsAndAnswers,
    string? Result,
    IReadOnlyList<CreateNegotiationResultDto>? Results = null);

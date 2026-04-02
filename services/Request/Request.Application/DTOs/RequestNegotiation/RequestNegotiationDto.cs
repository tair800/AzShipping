namespace Request.Application.DTOs.RequestNegotiation;

public record RequestNegotiationDto(
    Guid Id,
    Guid ClientId,
    string? ClientName,
    Guid? WayOfNegotiationId,
    string? WayOfNegotiationName,
    DateTime CreationDate,
    string? Question,
    string? Answer,
    string? Result);

namespace Request.Application.DTOs.RequestNegotiation;

public record UpdateRequestNegotiationDto(
    Guid? ClientId = null,
    string? ClientName = null,
    Guid? WayOfNegotiationId = null,
    string? WayOfNegotiationName = null,
    string? Question = null,
    string? Answer = null,
    string? Result = null);

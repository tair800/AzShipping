namespace Clients.Application.DTOs.Negotiation;

public record NegotiationResultDto(Guid Id, Guid NegotiationId, string Result, string? Comments, DateTime ResultDate);
public record CreateNegotiationResultDto(string Result, string? Comments, string? ResultDate);  // ResultDate "yyyy-MM-dd"
public record UpdateNegotiationResultDto(string Result, string? Comments, string? ResultDate);

namespace Carrier.Application.DTOs.CarrierDocument;

public record CarrierDocumentDto
{
    public Guid Id { get; init; }
    public Guid CarrierId { get; init; }
    public string DocumentNumber { get; init; } = string.Empty;
    public DateTime DocumentDate { get; init; }
    public string DocumentName { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? ExpirationDate { get; init; }
    public string? Comments { get; init; }
    public bool AvailableForClient { get; init; }
    public bool IsSent { get; init; }
    public string? FilePath { get; init; }
}

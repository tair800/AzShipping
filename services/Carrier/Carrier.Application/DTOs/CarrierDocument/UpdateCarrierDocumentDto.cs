namespace Carrier.Application.DTOs.CarrierDocument;

public record UpdateCarrierDocumentDto
{
    public string DocumentNumber { get; init; } = string.Empty;
    public DateTime DocumentDate { get; init; }
    public string DocumentName { get; init; } = string.Empty;
    public DateTime? ExpirationDate { get; init; }
    public string? Comments { get; init; }
    public bool AvailableForClient { get; init; }
    public bool IsSent { get; init; }
    public string? FilePath { get; init; }
}

namespace Clients.Application.DTOs.Document;

public record DocumentDto
{
    public Guid Id { get; init; }
    public Guid ClientId { get; init; }
    public Guid? CompanyId { get; init; }
    public string DocumentType { get; init; } = "upload";
    public Guid? TemplateId { get; init; }
    public string DocumentNumber { get; init; } = string.Empty;
    public DateTime DocumentDate { get; init; }
    public string DocumentName { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? ValidFrom { get; init; }
    public DateTime? ValidUntil { get; init; }
    public DateTime? ExpirationDate { get; init; }
    public Guid? NotifyUserId { get; init; }
    public bool ProhibitOnExpiry { get; init; }
    public bool IsDefault { get; init; }
    public string? Comments { get; init; }
    public bool AvailableForClient { get; init; }
    public bool IsSent { get; init; }
    public string? FilePath { get; init; }
}

namespace Settings.Application.DTOs.SalesFunnelStatus;

public record SalesFunnelStatusDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int StatusPosition { get; init; }
    public Guid? ResponsibleManagerId { get; init; }
    public int NumberOfDays { get; init; }
    public bool SendToEmail { get; init; }
    public bool SendNotification { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

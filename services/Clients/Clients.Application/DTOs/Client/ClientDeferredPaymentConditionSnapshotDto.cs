namespace Clients.Application.DTOs.Client;

/// <summary>Snapshot from Settings <c>DeferredPaymentConditions</c> (read-only on client responses).</summary>
public record ClientDeferredPaymentConditionSnapshotDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? FullText { get; init; }
    public int? ClientDaysOfDelay { get; init; }
    public bool IsActive { get; init; }
}

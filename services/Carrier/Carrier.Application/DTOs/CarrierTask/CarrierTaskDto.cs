namespace Carrier.Application.DTOs.CarrierTask;

public record CarrierTaskDto
{
    public Guid Id { get; init; }
    public Guid ProjectId { get; init; }
    public string ProjectName { get; init; } = string.Empty;
    public Guid CarrierId { get; init; }  // From Project, for convenience
    public string TaskNo { get; init; } = string.Empty;
    public DateTime DateOfCreation { get; init; }
    public Guid? ResponsibleUserId { get; init; }
    public string TaskName { get; init; } = string.Empty;
    public Guid? TaskPriorityId { get; init; }
    public Guid? TaskStatusId { get; init; }
    public DateTime? Deadline { get; init; }
    /// <summary>Countdown to deadline (HH:mm:ss). Computed from Deadline - UtcNow. Null if no deadline or overdue.
    /// </summary>
    public string? TimerCountdown { get; init; }
}

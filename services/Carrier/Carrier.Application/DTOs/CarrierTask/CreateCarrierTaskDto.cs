namespace Carrier.Application.DTOs.CarrierTask;

public record CreateCarrierTaskDto
{
    public Guid? ProjectId { get; init; }  // If null, use default project for carrier (auto-created)
    public Guid? ResponsibleUserId { get; init; }
    public string TaskName { get; init; } = string.Empty;
    public Guid? TaskPriorityId { get; init; }
    public Guid? TaskStatusId { get; init; }
    public DateTime? Deadline { get; init; }
}

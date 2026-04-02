namespace General.Application.DTOs.Task;

public record UpdateTaskDto
{
    public string TaskName { get; init; } = string.Empty;
    public int? RelatedModule { get; init; }
    public Guid? RelatedRecordId { get; init; }
    public Guid? OperationId { get; init; }
    public Guid? ClientId { get; init; }
    public Guid? ProjectId { get; init; }
    public long? ResponsibleUserId { get; init; }
    public Guid? PriorityId { get; init; }
    public Guid? StatusId { get; init; }
    public DateTime? Deadline { get; init; }
    public DateTime? RemindAt { get; init; }
    public string? Comments { get; init; }
}

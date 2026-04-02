namespace General.Application.DTOs.Task;

public record CreateTaskDto
{
    public int TaskType { get; init; }  // 0=Client, 1=Internal
    public string TaskName { get; init; } = string.Empty;

    /// <summary>Internal task: module (Operations, Quotes, Requests, …). Optional if legacy OperationId/ProjectId used.</summary>
    public int? RelatedModule { get; init; }

    /// <summary>Target entity id for the selected module.</summary>
    public Guid? RelatedRecordId { get; init; }

    public Guid? OperationId { get; init; }   // Client task: will be created in future
    public Guid? ClientId { get; init; }      // Client task: from chosen operation
    public Guid? ProjectId { get; init; }     // Null for now
    public long? ResponsibleUserId { get; init; }
    public Guid? PriorityId { get; init; }
    public Guid? StatusId { get; init; }
    public DateTime? Deadline { get; init; }
    public DateTime? RemindAt { get; init; }
    public string? Comments { get; init; }
}

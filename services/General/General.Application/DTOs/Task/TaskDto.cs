namespace General.Application.DTOs.Task;

public record TaskDto
{
    public Guid Id { get; init; }
    public string TaskNo { get; init; } = string.Empty;
    public DateTime DateOfCreation { get; init; }
    public int TaskType { get; init; }  // 0=Client, 1=Internal
    public string TaskName { get; init; } = string.Empty;

    public Guid? OperationId { get; init; }
    public Guid? ClientId { get; init; }
    public Guid? ProjectId { get; init; }
    public string ProjectName { get; init; } = string.Empty;

    public int RelatedModule { get; init; }
    public Guid? RelatedRecordId { get; init; }
    public string RelatedModuleLabel { get; init; } = string.Empty;

    public long? ResponsibleUserId { get; init; }
    public string? ResponsiblePersonName { get; init; }

    public Guid? PriorityId { get; init; }
    public Guid? StatusId { get; init; }

    public DateTime? Deadline { get; init; }
    public DateTime? RemindAt { get; init; }
    public string? TimerCountdown { get; init; }  // Computed from Deadline

    public string? Comments { get; init; }
    public IReadOnlyList<TaskDocumentDto> Documents { get; init; } = [];
}

public record TaskDocumentDto
{
    public Guid Id { get; init; }
    public string FilePath { get; init; } = string.Empty;
    public string? DocumentName { get; init; }
}

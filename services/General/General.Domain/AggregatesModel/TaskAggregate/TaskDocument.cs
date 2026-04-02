namespace General.Domain.AggregatesModel.TaskAggregate;

/// <summary>
/// Document attached to a task.
/// </summary>
public class TaskDocument
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public GeneralTask Task { get; set; } = null!;
    public string FilePath { get; set; } = string.Empty;
    public string? DocumentName { get; set; }
    public DateTime CreatedAt { get; set; }
}

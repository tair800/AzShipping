namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

/// <summary>
/// Task belongs to a project. Carrier is reached only through Project.Carrier.
/// TaskPriorityId and TaskStatusId prepared for future TaskPriorities/TaskStatuses from Settings.
/// </summary>
public class ProjectTask
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string TaskNo { get; set; } = string.Empty;        // Auto-generated e.g. TASK-0001
    public DateTime DateOfCreation { get; set; }
    public Guid? ResponsibleUserId { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public Guid? TaskPriorityId { get; set; }
    public Guid? TaskStatusId { get; set; }
    public DateTime? Deadline { get; set; }

    public Project Project { get; set; } = null!;
}

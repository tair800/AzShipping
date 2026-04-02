using General.Domain.AggregatesModel.ProjectAggregate;

namespace General.Domain.AggregatesModel.TaskAggregate;

/// <summary>
/// General task - task name, project, responsible person, priority, deadline, remind, documents, comments.
/// </summary>
public class GeneralTask
{
    public Guid Id { get; set; }
    public string TaskNo { get; set; } = string.Empty;           // Auto-generated e.g. 1234
    public DateTime DateOfCreation { get; set; }
    public TaskType TaskType { get; set; }
    public string TaskName { get; set; } = string.Empty;

    public Guid? OperationId { get; set; }                      // Client task: will be created in future
    public Guid? ClientId { get; set; }                         // Client task: from chosen operation

    public Guid? ProjectId { get; set; }                        // Null for now
    public Project? Project { get; set; }

    /// <summary>Internal tasks: business area (Operations, Quotes, …). <see cref="RelatedRecordId"/> is the target entity id.</summary>
    public TaskRelatedModule RelatedModule { get; set; }

    /// <summary>Target record id when <see cref="RelatedModule"/> is Quotes, Requests, Carriers, etc.; otherwise mirrors Operation/Project/Client id when applicable.</summary>
    public Guid? RelatedRecordId { get; set; }

    /// <summary>Identity <c>User.Id</c> (<c>long</c>). JWT <c>uid</c> is this value as a string. Match <c>Employee.UserId</c> when assignee is an employee.</summary>
    public long? ResponsibleUserId { get; set; }
    public Guid? PriorityId { get; set; }                         // From Settings (empty for now)
    public Guid? StatusId { get; set; }                           // From Settings (In Progress, Completed, On Hold)

    public DateTime? Deadline { get; set; }
    public DateTime? RemindAt { get; set; }                       // Remind - calendar with clock

    public string? Comments { get; set; }

    public ICollection<TaskDocument> Documents { get; set; } = new List<TaskDocument>();
}

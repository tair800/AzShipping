using General.Domain.AggregatesModel.TaskAggregate;

namespace General.Domain.AggregatesModel.ProjectAggregate;

/// <summary>
/// Project - belongs to General service. Tasks are linked to projects.
/// </summary>
public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ICollection<GeneralTask> Tasks { get; set; } = new List<GeneralTask>();
}

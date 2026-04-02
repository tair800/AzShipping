namespace General.Domain.AggregatesModel.TaskAggregate;

/// <summary>Where an <see cref="TaskType.Internal"/> task is anchored (Figma &quot;Select project&quot;).</summary>
public enum TaskRelatedModule
{
    None = 0,
    Operations = 1,
    Quotes = 2,
    Requests = 3,
    Projects = 4,
    Clients = 5,
    Carriers = 6
}

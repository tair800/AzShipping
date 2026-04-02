namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

/// <summary>
/// Project belongs to a carrier. Tasks relate to carrier only through project.
/// </summary>
public class Project
{
    public Guid Id { get; set; }
    public Guid CarrierId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public Carrier Carrier { get; set; } = null!;
    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
}

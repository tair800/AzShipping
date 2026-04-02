namespace Clients.Domain.AggregatesModel.ClientAggregate;

public class ClientContactPerson
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public string? EnglishName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string? Fax { get; set; }
    /// <summary>Job title / position from Settings worker posts (optional).</summary>
    public Guid? WorkerPostId { get; set; }
}

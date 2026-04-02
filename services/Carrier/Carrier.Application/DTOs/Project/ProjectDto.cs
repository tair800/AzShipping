namespace Carrier.Application.DTOs.Project;

public record ProjectDto
{
    public Guid Id { get; init; }
    public Guid CarrierId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}

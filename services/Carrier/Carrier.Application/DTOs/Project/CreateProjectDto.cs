namespace Carrier.Application.DTOs.Project;

public record CreateProjectDto
{
    public string Name { get; init; } = string.Empty;
}

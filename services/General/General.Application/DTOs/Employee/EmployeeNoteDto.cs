namespace General.Application.DTOs.Employee;

public record EmployeeNoteDto
{
    public Guid Id { get; init; }
    public Guid EmployeeId { get; init; }
    public string Content { get; init; } = "";
    public DateOnly? NoteDate { get; init; }
    public DateTime CreatedAtUtc { get; init; }
}

namespace General.Domain.AggregatesModel.EmployeeAggregate;

public class EmployeeNote
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }

    /// <summary>Free-text comment shown in the UI.</summary>
    public string Content { get; set; } = "";

    /// <summary>Calendar date in UTC, set automatically when the note is created.</summary>
    public DateOnly? NoteDate { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}

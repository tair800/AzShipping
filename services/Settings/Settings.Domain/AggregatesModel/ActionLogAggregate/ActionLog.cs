namespace Settings.Domain.AggregatesModel.ActionLogAggregate;

public class ActionLog
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Action { get; set; } = string.Empty;   // e.g. "Request changed", "Request created"
    public string Data { get; set; } = string.Empty;     // JSON or text details
    public string? SessionId { get; set; }
    public string? IpAddress { get; set; }
    public string? Location { get; set; }
    public string? Browser { get; set; }
    public Guid? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
}

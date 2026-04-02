namespace General.Domain.AggregatesModel.MeetingHistoryAggregate;

public class MeetingHistory
{
    public Guid Id { get; set; }
    public Guid MeetingId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
    public string? Time { get; set; }
    public Guid? EventResultId { get; set; }
    public string? FieldName { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime CreatedAt { get; set; }
}

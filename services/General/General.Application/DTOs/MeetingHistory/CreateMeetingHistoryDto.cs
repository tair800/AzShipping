namespace General.Application.DTOs.MeetingHistory;

public class CreateMeetingHistoryDto
{
    public Guid MeetingId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
    public string? Time { get; set; }
    public Guid? EventResultId { get; set; }
}

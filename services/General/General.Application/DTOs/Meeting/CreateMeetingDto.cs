namespace General.Application.DTOs.Meeting;

public class CreateMeetingDto
{
    public string Name { get; set; } = string.Empty;
    public Guid? MeetingTypeId { get; set; }
    public Guid? MeetingResultId { get; set; }
    public Guid? MeetingStatusId { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? TaskId { get; set; }
    public Guid? OperationId { get; set; }
    public Guid? MeetingPriorityId { get; set; }
    public DateTime? Date { get; set; }
    public string? Time { get; set; }
    public string? Address { get; set; }
    public string? Comments { get; set; }
    public bool HasClient { get; set; }
}

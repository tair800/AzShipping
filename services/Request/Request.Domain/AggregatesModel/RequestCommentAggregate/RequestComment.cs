namespace Request.Domain.AggregatesModel.RequestCommentAggregate;

public class RequestComment
{
    public Guid Id { get; set; }
    public Guid RequestId { get; set; }
    public string? Comments { get; set; }
    public DateTime Date { get; set; }
}

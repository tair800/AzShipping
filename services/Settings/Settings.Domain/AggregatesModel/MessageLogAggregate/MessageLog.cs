namespace Settings.Domain.AggregatesModel.MessageLogAggregate;

public class MessageLog
{
    public long Id { get; set; }
    public DateTime SentAt { get; set; }
    public string Sender { get; set; } = string.Empty;    // email
    public string Receiver { get; set; } = string.Empty;   // email
    public string? CompanyName { get; set; }
    public string Theme { get; set; } = string.Empty;      // subject
    public string Body { get; set; } = string.Empty;
    public string? LinkUrl { get; set; }                   // e.g. link to order
    public string? LinkText { get; set; }
}

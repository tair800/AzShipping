namespace Settings.Domain.AggregatesModel.SystemLogAggregate;

public class SystemLog
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; } = string.Empty;  // Source/category (e.g. "google api", "email")
    public string Level { get; set; } = string.Empty; // Information, Warning, Error, Debug
    public string Body { get; set; } = string.Empty;  // Message content
}

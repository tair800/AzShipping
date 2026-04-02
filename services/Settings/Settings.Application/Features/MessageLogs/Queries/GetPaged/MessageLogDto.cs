namespace Settings.Application.Features.MessageLogs.Queries.GetPaged;

public sealed record MessageLogDto(
    long Id,
    DateTime SentAt,
    string Sender,
    string Receiver,
    string? CompanyName,
    string Theme,
    string Body,
    string? LinkUrl,
    string? LinkText);

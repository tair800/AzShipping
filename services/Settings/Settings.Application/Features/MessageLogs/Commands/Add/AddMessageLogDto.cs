namespace Settings.Application.Features.MessageLogs.Commands.Add;

public sealed record AddMessageLogDto(
    string Sender,
    string Receiver,
    string? CompanyName,
    string Theme,
    string Body,
    string? LinkUrl,
    string? LinkText);

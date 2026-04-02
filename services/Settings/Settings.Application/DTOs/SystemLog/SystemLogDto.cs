namespace Settings.Application.DTOs.SystemLog;

public record SystemLogDto(long Id, DateTime CreatedAt, string Name, string Level, string Body);

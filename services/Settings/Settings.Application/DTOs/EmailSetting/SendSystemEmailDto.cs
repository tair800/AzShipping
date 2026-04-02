namespace Settings.Application.DTOs.EmailSetting;

public record SendSystemEmailDto(string To, string Subject, string Body, bool IsHtml = true);

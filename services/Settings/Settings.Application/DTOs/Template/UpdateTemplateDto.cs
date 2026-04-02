namespace Settings.Application.DTOs.Template;

public record UpdateTemplateDto(string Name, bool IsActive, Dictionary<string, string>? Translations = null);

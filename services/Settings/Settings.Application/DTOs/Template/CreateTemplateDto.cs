namespace Settings.Application.DTOs.Template;

public record CreateTemplateDto(string Name, bool IsActive, Dictionary<string, string>? Translations = null);

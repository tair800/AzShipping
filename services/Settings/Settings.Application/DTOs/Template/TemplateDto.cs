namespace Settings.Application.DTOs.Template;

public record TemplateDto(Guid Id, string Name, bool IsActive, Dictionary<string, string> Translations, DateTime CreatedAt, DateTime? UpdatedAt);

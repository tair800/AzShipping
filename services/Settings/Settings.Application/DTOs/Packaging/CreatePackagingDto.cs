namespace Settings.Application.DTOs.Packaging;

public record CreatePackagingDto(string Name, bool IsActive, Dictionary<string, string>? Translations = null);

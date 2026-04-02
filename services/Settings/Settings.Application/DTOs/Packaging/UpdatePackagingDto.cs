namespace Settings.Application.DTOs.Packaging;

public record UpdatePackagingDto(string Name, bool IsActive, Dictionary<string, string>? Translations = null);

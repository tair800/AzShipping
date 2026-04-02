namespace Settings.Application.DTOs.LoadingMethod;

public record UpdateLoadingMethodDto(string Name, bool IsActive, Dictionary<string, string>? Translations = null);

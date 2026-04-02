namespace Settings.Application.DTOs.LoadingMethod;

public record CreateLoadingMethodDto(string Name, bool IsActive, Dictionary<string, string>? Translations = null);

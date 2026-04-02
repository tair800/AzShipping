namespace Settings.Application.DTOs.LoadingMethod;

public record LoadingMethodDto(Guid Id, string Name, bool IsActive, Dictionary<string, string> Translations, DateTime CreatedAt, DateTime? UpdatedAt);

namespace Settings.Application.DTOs.Packaging;

public record PackagingDto(Guid Id, string Name, bool IsActive, Dictionary<string, string> Translations, DateTime CreatedAt, DateTime? UpdatedAt);

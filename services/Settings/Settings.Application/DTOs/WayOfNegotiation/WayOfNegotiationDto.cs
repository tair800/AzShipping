namespace Settings.Application.DTOs.WayOfNegotiation;

public record WayOfNegotiationDto(Guid Id, string Name, bool IsActive, Dictionary<string, string> Translations, DateTime CreatedAt, DateTime? UpdatedAt);

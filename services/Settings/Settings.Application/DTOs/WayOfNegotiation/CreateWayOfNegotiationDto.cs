namespace Settings.Application.DTOs.WayOfNegotiation;

public record CreateWayOfNegotiationDto(string Name, bool IsActive, Dictionary<string, string>? Translations = null);

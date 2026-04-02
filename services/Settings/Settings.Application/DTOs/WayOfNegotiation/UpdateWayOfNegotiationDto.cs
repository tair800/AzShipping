namespace Settings.Application.DTOs.WayOfNegotiation;

public record UpdateWayOfNegotiationDto(string Name, bool IsActive, Dictionary<string, string>? Translations = null);

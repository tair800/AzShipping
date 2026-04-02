namespace Settings.Application.DTOs.TransportType;

public record UpdateTransportTypeDto(string Name, bool IsAir, bool IsSea, bool IsRoad, bool IsRail, bool IsActive);

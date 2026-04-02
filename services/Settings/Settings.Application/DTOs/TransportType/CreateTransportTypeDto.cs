namespace Settings.Application.DTOs.TransportType;

public record CreateTransportTypeDto(string Name, bool IsAir, bool IsSea, bool IsRoad, bool IsRail, bool IsActive);

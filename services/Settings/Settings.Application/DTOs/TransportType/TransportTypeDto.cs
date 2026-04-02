namespace Settings.Application.DTOs.TransportType;

public record TransportTypeDto(Guid Id, string Name, bool IsAir, bool IsSea, bool IsRoad, bool IsRail, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);

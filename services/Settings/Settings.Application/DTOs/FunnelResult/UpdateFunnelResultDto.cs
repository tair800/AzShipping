namespace Settings.Application.DTOs.FunnelResult;

public record UpdateFunnelResultDto(string Name, Guid ResultTypeId, bool ToNextStep, bool IsActive);

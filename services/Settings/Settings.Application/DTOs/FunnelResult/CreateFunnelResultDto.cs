namespace Settings.Application.DTOs.FunnelResult;

public record CreateFunnelResultDto(string Name, Guid ResultTypeId, bool ToNextStep, bool IsActive);

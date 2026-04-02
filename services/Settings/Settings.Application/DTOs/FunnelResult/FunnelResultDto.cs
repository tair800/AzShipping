namespace Settings.Application.DTOs.FunnelResult;

public record FunnelResultDto(
    Guid Id,
    string Name,
    Guid ResultTypeId,
    string ResultTypeName,
    bool ToNextStep,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

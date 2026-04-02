namespace Request.Application.DTOs.Request;

public record CreateRequestTypeDto(
    string Code,
    string Name,
    string Direction,
    string Mode,
    string? SubType,
    string RequestNumberPrefix,
    string CarrierApiPath,
    string CarrierLabel,
    int SortOrder);

namespace Settings.Application.DTOs.DeferredPaymentCondition;

public record DeferredPaymentConditionDto(
    Guid Id,
    string Name,
    bool ClientIncluded,
    int? ClientDaysOfDelay,
    bool CarrierIncluded,
    int? CarrierDaysOfDelay,
    string? FullText,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

public record CreateDeferredPaymentConditionDto(
    string Name,
    bool ClientIncluded,
    int? ClientDaysOfDelay,
    bool CarrierIncluded,
    int? CarrierDaysOfDelay,
    string? FullText,
    bool IsActive);

public record UpdateDeferredPaymentConditionDto(
    string Name,
    bool ClientIncluded,
    int? ClientDaysOfDelay,
    bool CarrierIncluded,
    int? CarrierDaysOfDelay,
    string? FullText,
    bool IsActive);

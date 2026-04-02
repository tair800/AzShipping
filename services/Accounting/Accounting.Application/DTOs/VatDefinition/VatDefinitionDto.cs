namespace Accounting.Application.DTOs.VatDefinition;

public record VatDefinitionDto(
    Guid Id,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc,
    string Name,
    decimal Percent,
    bool IsAlcohol,
    string? BuyingAccountName,
    string BuyingAccountCode,
    string? SellingAccountName,
    string SellingAccountCode,
    string? Notes,
    bool IsActive);

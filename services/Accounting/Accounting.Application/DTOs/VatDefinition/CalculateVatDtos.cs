namespace Accounting.Application.DTOs.VatDefinition;

public record CalculateVatFromNetRequestDto(decimal AmountExcludingVat, Guid VatDefinitionId);

public record CalculateVatFromNetResultDto(
    decimal AmountExcludingVat,
    decimal VatPercent,
    decimal VatAmount,
    decimal AmountIncludingVat);

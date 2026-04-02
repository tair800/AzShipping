namespace Accounting.Application.DTOs.VatDefinition;

/// <summary>Backward-compatible shape for <c>/api/vatrates</c> (formerly Settings).</summary>
public record VatRateLegacyDto(Guid Id, string Name, decimal Rate, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);

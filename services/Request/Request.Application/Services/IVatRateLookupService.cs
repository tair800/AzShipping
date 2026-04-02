namespace Request.Application.Services;

/// <summary>
/// Looks up VAT rate percentage from the Settings service for price proposal calculations.
/// </summary>
public interface IVatRateLookupService
{
    /// <summary>
    /// Gets the VAT rate percentage (e.g. 18 for 18%) by ID. Returns null if not found.
    /// </summary>
    Task<decimal?> GetVatPercentAsync(Guid? vatRateId, CancellationToken cancellationToken = default);
}

using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Request.Application.Services;

namespace Request.Infrastructure.Services;

public class VatRateLookupServiceOptions
{
    /// <summary>Accounting.API base URL (VAT definitions and legacy <c>/api/vatrates</c>).</summary>
    public string AccountingBaseUrl { get; set; } = "http://localhost:5072";

    /// <summary>Settings.API base URL (action logs and other Settings calls).</summary>
    public string SettingsBaseUrl { get; set; } = "http://localhost:5064";
}

public sealed class VatRateLookupService(
    IHttpClientFactory httpClientFactory,
    IOptions<VatRateLookupServiceOptions> options,
    ILogger<VatRateLookupService> logger) : IVatRateLookupService
{
    public async Task<decimal?> GetVatPercentAsync(Guid? vatRateId, CancellationToken cancellationToken = default)
    {
        if (vatRateId == null || vatRateId == Guid.Empty)
            return null;

        var baseUrl = (options.Value.AccountingBaseUrl ?? "http://localhost:5072").TrimEnd('/');
        try
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{baseUrl}/api/vatrates/{vatRateId}", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning("VatRate lookup failed for {VatRateId}: HTTP {StatusCode}", vatRateId, response.StatusCode);
                return null;
            }

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (root.TryGetProperty("rate", out var rateEl))
                return rateEl.GetDecimal();
            if (root.TryGetProperty("Rate", out var rateCap))
                return rateCap.GetDecimal();
            return null;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "VatRate lookup failed for {VatRateId}", vatRateId);
            return null;
        }
    }
}

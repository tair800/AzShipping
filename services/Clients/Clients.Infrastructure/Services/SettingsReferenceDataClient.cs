using System.Net.Http.Json;
using System.Text.Json;
using Clients.Application.DTOs.Client;
using Clients.Application.Services;
using Microsoft.Extensions.Logging;

namespace Clients.Infrastructure.Services;

public sealed class SettingsReferenceDataClient(HttpClient httpClient, ILogger<SettingsReferenceDataClient> logger) : ISettingsReferenceDataClient
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<IReadOnlyDictionary<Guid, ClientBankDetailsDto>> ResolveBanksAsync(
        IReadOnlyCollection<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        if (ids.Count == 0)
            return new Dictionary<Guid, ClientBankDetailsDto>();

        try
        {
            var list = await httpClient.GetFromJsonAsync<List<ClientBankDetailsDto>>("api/banks", JsonOptions, cancellationToken);
            if (list == null || list.Count == 0)
                return new Dictionary<Guid, ClientBankDetailsDto>();

            var idSet = ids.ToHashSet();
            return list.Where(b => idSet.Contains(b.Id)).ToDictionary(b => b.Id);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to load banks from Settings");
            return new Dictionary<Guid, ClientBankDetailsDto>();
        }
    }

    public async Task<IReadOnlyDictionary<Guid, ClientDeferredPaymentConditionSnapshotDto>> ResolveDeferredPaymentConditionsAsync(
        IReadOnlyCollection<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        if (ids.Count == 0)
            return new Dictionary<Guid, ClientDeferredPaymentConditionSnapshotDto>();

        try
        {
            var list = await httpClient.GetFromJsonAsync<List<DeferredPaymentConditionJson>>("api/deferredpaymentconditions", JsonOptions, cancellationToken);
            if (list == null || list.Count == 0)
                return new Dictionary<Guid, ClientDeferredPaymentConditionSnapshotDto>();

            var idSet = ids.ToHashSet();
            return list
                .Where(x => idSet.Contains(x.Id))
                .ToDictionary(x => x.Id, x => new ClientDeferredPaymentConditionSnapshotDto
                {
                    Id = x.Id,
                    Name = x.Name ?? string.Empty,
                    FullText = x.FullText,
                    ClientDaysOfDelay = x.ClientDaysOfDelay,
                    IsActive = x.IsActive
                });
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to load deferred payment conditions from Settings");
            return new Dictionary<Guid, ClientDeferredPaymentConditionSnapshotDto>();
        }
    }

    private sealed class DeferredPaymentConditionJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? FullText { get; set; }
        public int? ClientDaysOfDelay { get; set; }
        public bool IsActive { get; set; }
    }
}

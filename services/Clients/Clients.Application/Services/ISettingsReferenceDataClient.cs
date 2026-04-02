using Clients.Application.DTOs.Client;

namespace Clients.Application.Services;

/// <summary>Reads reference data from the Settings service (banks, payment terms).</summary>
public interface ISettingsReferenceDataClient
{
    Task<IReadOnlyDictionary<Guid, ClientBankDetailsDto>> ResolveBanksAsync(
        IReadOnlyCollection<Guid> ids,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyDictionary<Guid, ClientDeferredPaymentConditionSnapshotDto>> ResolveDeferredPaymentConditionsAsync(
        IReadOnlyCollection<Guid> ids,
        CancellationToken cancellationToken = default);
}

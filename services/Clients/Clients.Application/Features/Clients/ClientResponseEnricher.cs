using Clients.Application.DTOs.Client;
using Clients.Application.Services;

namespace Clients.Application.Features.Clients;

public static class ClientResponseEnricher
{
    public static async Task<ClientDto> EnrichFromSettingsAsync(
        ClientDto dto,
        ISettingsReferenceDataClient settings,
        CancellationToken cancellationToken = default)
    {
        var bankIds = dto.BankAccounts
            .SelectMany(ba => new[] { ba.BankId, ba.CorrespondentBankId })
            .Where(x => x.HasValue)
            .Select(x => x!.Value)
            .Distinct()
            .ToList();

        IReadOnlyCollection<Guid> dpIds = dto.Payment.DeferredPaymentConditionId is { } dpId
            ? new[] { dpId }
            : Array.Empty<Guid>();

        var bankMap = bankIds.Count > 0
            ? await settings.ResolveBanksAsync(bankIds, cancellationToken)
            : new Dictionary<Guid, ClientBankDetailsDto>();

        var dpMap = dpIds.Count > 0
            ? await settings.ResolveDeferredPaymentConditionsAsync(dpIds, cancellationToken)
            : new Dictionary<Guid, ClientDeferredPaymentConditionSnapshotDto>();

        var accounts = dto.BankAccounts.Select(ba => ba with
        {
            BankDetails = ba.BankId is { } bid && bankMap.TryGetValue(bid, out var b) ? b : null,
            CorrespondentBankDetails = ba.CorrespondentBankId is { } cid && bankMap.TryGetValue(cid, out var c) ? c : null
        }).ToList();

        var payment = dto.Payment with
        {
            DeferredPaymentCondition = dto.Payment.DeferredPaymentConditionId is { } pid && dpMap.TryGetValue(pid, out var p)
                ? p
                : null
        };

        return dto with { BankAccounts = accounts, Payment = payment };
    }

    public static async Task<IReadOnlyList<ClientDto>> EnrichFromSettingsAsync(
        IReadOnlyList<ClientDto> list,
        ISettingsReferenceDataClient settings,
        CancellationToken cancellationToken = default)
    {
        var bankIds = list
            .SelectMany(dto => dto.BankAccounts.SelectMany(ba => new[] { ba.BankId, ba.CorrespondentBankId }))
            .Where(x => x.HasValue)
            .Select(x => x!.Value)
            .Distinct()
            .ToList();

        var dpIds = list
            .Select(dto => dto.Payment.DeferredPaymentConditionId)
            .Where(x => x.HasValue)
            .Select(x => x!.Value)
            .Distinct()
            .ToList();

        var bankMap = bankIds.Count > 0
            ? await settings.ResolveBanksAsync(bankIds, cancellationToken)
            : new Dictionary<Guid, ClientBankDetailsDto>();

        var dpMap = dpIds.Count > 0
            ? await settings.ResolveDeferredPaymentConditionsAsync(dpIds, cancellationToken)
            : new Dictionary<Guid, ClientDeferredPaymentConditionSnapshotDto>();

        return list.Select(dto =>
        {
            var accounts = dto.BankAccounts.Select(ba => ba with
            {
                BankDetails = ba.BankId is { } bid && bankMap.TryGetValue(bid, out var b) ? b : null,
                CorrespondentBankDetails = ba.CorrespondentBankId is { } cid && bankMap.TryGetValue(cid, out var c) ? c : null
            }).ToList();

            var payment = dto.Payment with
            {
                DeferredPaymentCondition = dto.Payment.DeferredPaymentConditionId is { } pid && dpMap.TryGetValue(pid, out var p)
                    ? p
                    : null
            };

            return dto with { BankAccounts = accounts, Payment = payment };
        }).ToList();
    }
}

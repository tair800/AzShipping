using Carrier.Application.DTOs.Carrier;
using Carrier.Application.Features.Carriers;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.Carriers.Commands.Update;

public sealed class UpdateCarrierCommandHandler(ICarrierRepository repository, IActionLogClient actionLogClient) : IRequestHandler<UpdateCarrierCommand, CarrierDto?>
{
    private static DateTime? ToUtc(DateTime? d) =>
        d == null ? null : d.Value.Kind == DateTimeKind.Utc ? d : DateTime.SpecifyKind(d.Value, DateTimeKind.Utc);

    public async Task<CarrierDto?> Handle(UpdateCarrierCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;
        entity.Name = dto.Name;
        entity.LocalName = dto.LocalName;
        entity.ClientAdsCode = dto.ClientAdsCode;
        entity.Okpo = dto.Okpo;
        entity.Bin = dto.Bin;
        entity.Ogrn = dto.Ogrn;
        entity.Tin = dto.Tin;
        entity.Rrc = dto.Rrc;
        entity.VatNumber = dto.VatNumber;
        entity.CarrierTypeId = dto.CarrierTypeId;
        entity.TransportTypeId = dto.TransportTypeId;
        entity.CarrierDirection = dto.CarrierDirection;
        entity.DateOfCreation = ToUtc(dto.DateOfCreation);
        entity.LegalCountryId = dto.LegalCountryId;
        entity.LegalStateId = dto.LegalStateId;
        entity.LegalCityId = dto.LegalCityId;
        entity.LegalZipCode = dto.LegalZipCode;
        entity.LegalPhones = dto.LegalPhones;
        entity.LegalFax = dto.LegalFax;
        entity.LegalEmails = dto.LegalEmails;
        entity.PostalCountryId = dto.PostalCountryId;
        entity.PostalStateId = dto.PostalStateId;
        entity.PostalCityId = dto.PostalCityId;
        entity.PostalZipCode = dto.PostalZipCode;
        entity.PostalPhones = dto.PostalPhones;
        entity.PostalFax = dto.PostalFax;
        entity.PostalEmails = dto.PostalEmails;
        entity.CreditLimit = dto.CreditLimit;
        entity.PaymentDelay = dto.PaymentDelay;
        entity.DeferredPaymentConditionId = dto.DeferredPaymentConditionId;
        entity.Comment = dto.Comment;
        entity.IsDeactive = dto.IsDeactive;
        entity.UpdatedAt = DateTime.UtcNow;

        entity.ContactPersons.Clear();
        foreach (var cp in dto.ContactPersons)
        {
            entity.ContactPersons.Add(new CarrierContactPerson
            {
                Id = cp.Id ?? Guid.NewGuid(),
                CarrierId = entity.Id,
                EnglishName = cp.EnglishName,
                Position = cp.Position,
                Emails = cp.Emails,
                Phones = cp.Phones,
                Fax = cp.Fax
            });
        }

        entity.BankAccounts.Clear();
        foreach (var ba in dto.BankAccounts)
        {
            entity.BankAccounts.Add(new CarrierBankAccount
            {
                Id = ba.Id ?? Guid.NewGuid(),
                CarrierId = entity.Id,
                CurrencyCode = ba.CurrencyCode,
                AccountNumber = ba.AccountNumber,
                BankId = ba.BankId,
                TransitAccount = ba.TransitAccount,
                CorrespondentBank = ba.CorrespondentBank,
                CorrespondentAccount = ba.CorrespondentAccount
            });
        }

        entity.Managers.Clear();
        foreach (var userId in dto.ManagerIds)
        {
            entity.Managers.Add(new CarrierManager
            {
                Id = Guid.NewGuid(),
                CarrierId = entity.Id,
                UserId = userId
            });
        }

        await repository.UpdateWithChildrenAsync(entity, cancellationToken);
        var updated = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var result = CarrierMapper.MapToDto(updated!);
        await actionLogClient.LogAsync("Carrier updated", $"carrier: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}

using Carrier.Application.DTOs.Carrier;
using Carrier.Application.Features.Carriers;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.Carriers.Commands.Create;

public sealed class CreateCarrierCommandHandler(ICarrierRepository repository, IActionLogClient actionLogClient) : IRequestHandler<CreateCarrierCommand, CarrierDto>
{
    private static DateTime? ToUtc(DateTime? d) =>
        d == null ? null : d.Value.Kind == DateTimeKind.Utc ? d : DateTime.SpecifyKind(d.Value, DateTimeKind.Utc);

    public async Task<CarrierDto> Handle(CreateCarrierCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new Carrier.Domain.AggregatesModel.CarrierAggregate.Carrier
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            LocalName = dto.LocalName,
            ClientAdsCode = dto.ClientAdsCode,
            Okpo = dto.Okpo,
            Bin = dto.Bin,
            Ogrn = dto.Ogrn,
            Tin = dto.Tin,
            Rrc = dto.Rrc,
            VatNumber = dto.VatNumber,
            CarrierTypeId = dto.CarrierTypeId,
            TransportTypeId = dto.TransportTypeId,
            CarrierDirection = dto.CarrierDirection,
            DateOfCreation = ToUtc(dto.DateOfCreation),
            LegalCountryId = dto.LegalCountryId,
            LegalStateId = dto.LegalStateId,
            LegalCityId = dto.LegalCityId,
            LegalZipCode = dto.LegalZipCode,
            LegalPhones = dto.LegalPhones,
            LegalFax = dto.LegalFax,
            LegalEmails = dto.LegalEmails,
            PostalCountryId = dto.PostalCountryId,
            PostalStateId = dto.PostalStateId,
            PostalCityId = dto.PostalCityId,
            PostalZipCode = dto.PostalZipCode,
            PostalPhones = dto.PostalPhones,
            PostalFax = dto.PostalFax,
            PostalEmails = dto.PostalEmails,
            CreditLimit = dto.CreditLimit,
            PaymentDelay = dto.PaymentDelay,
            DeferredPaymentConditionId = dto.DeferredPaymentConditionId,
            Comment = dto.Comment,
            IsDeactive = dto.IsDeactive,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var cp in dto.ContactPersons)
        {
            entity.ContactPersons.Add(new CarrierContactPerson
            {
                Id = Guid.NewGuid(),
                CarrierId = entity.Id,
                EnglishName = cp.EnglishName,
                Position = cp.Position,
                Emails = cp.Emails,
                Phones = cp.Phones,
                Fax = cp.Fax
            });
        }

        foreach (var ba in dto.BankAccounts)
        {
            entity.BankAccounts.Add(new CarrierBankAccount
            {
                Id = Guid.NewGuid(),
                CarrierId = entity.Id,
                CurrencyCode = ba.CurrencyCode,
                AccountNumber = ba.AccountNumber,
                BankId = ba.BankId,
                TransitAccount = ba.TransitAccount,
                CorrespondentBank = ba.CorrespondentBank,
                CorrespondentAccount = ba.CorrespondentAccount
            });
        }

        foreach (var userId in dto.ManagerIds)
        {
            entity.Managers.Add(new CarrierManager
            {
                Id = Guid.NewGuid(),
                CarrierId = entity.Id,
                UserId = userId
            });
        }

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var result = CarrierMapper.MapToDto(created!);
        await actionLogClient.LogAsync("Carrier created", $"carrier: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}

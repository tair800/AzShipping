using MediatR;
using Settings.Application.DTOs.Company;
using Settings.Domain.AggregatesModel.CompanyAggregate;

namespace Settings.Application.Features.Companies;

public sealed record GetAllCompaniesQuery : IRequest<IReadOnlyList<CompanyDto>>;
public sealed class GetAllCompaniesQueryHandler(ICompanyRepository repository) : IRequestHandler<GetAllCompaniesQuery, IReadOnlyList<CompanyDto>>
{
    public async Task<IReadOnlyList<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(CompanyMapping.MapToDto).ToList();
    }
}

public sealed record GetCompanyByIdQuery(Guid Id) : IRequest<CompanyDto?>;
public sealed class GetCompanyByIdQueryHandler(ICompanyRepository repository) : IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
{
    public async Task<CompanyDto?> Handle(GetCompanyByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        return e == null ? null : CompanyMapping.MapToDto(e);
    }
}

public sealed record CreateCompanyCommand(CreateCompanyDto Dto) : IRequest<CompanyDto>;
public sealed class CreateCompanyCommandHandler(ICompanyRepository repository) : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken ct)
    {
        var d = request.Dto;
        var entity = new Company
        {
            Id = Guid.NewGuid(),
            Name = d.Name,
            NameFull = d.NameFull,
            DirectorsFullName = d.DirectorsFullName,
            InTheNameOfWhom = d.InTheNameOfWhom,
            WorkerPostId = d.WorkerPostId,
            Post = d.Post,
            VatRate = d.VatRate,
            PricingTypeId = d.PricingTypeId,
            PricingType = d.PricingType,
            CompanyPrefix = d.CompanyPrefix,
            CompanyCodeType = d.CompanyCodeType,
            CompanyCode = d.CompanyCode,
            VatCode = d.VatCode,
            Rrc = d.Rrc,
            CorrespondentAccount = d.CorrespondentAccount,
            Okpo = d.Okpo,
            Ogrn = d.Ogrn,
            CountryId = d.CountryId,
            StateId = d.StateId,
            CityId = d.CityId,
            Address = d.Address,
            PostCode = d.PostCode,
            Telephone = d.Telephone,
            Fax = d.Fax,
            Email = d.Email,
            Website = d.Website,
            IsMainCompany = d.IsMainCompany,
            CorrespondentCountryId = d.CorrespondentCountryId,
            CorrespondentStateId = d.CorrespondentStateId,
            CorrespondentCityId = d.CorrespondentCityId,
            CorrespondentAddress = d.CorrespondentAddress,
            CorrespondentPostCode = d.CorrespondentPostCode,
            CorrespondentTelephone = d.CorrespondentTelephone,
            CorrespondentFax = d.CorrespondentFax,
            CorrespondentEmail = d.CorrespondentEmail,
            CorrespondentWebsite = d.CorrespondentWebsite,
            IsActive = d.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        if (d.BankAccounts is { Count: > 0 })
            foreach (var b in d.BankAccounts)
                entity.BankAccounts.Add(new CompanyBankAccount { Id = Guid.NewGuid(), CompanyId = entity.Id, BankId = b.BankId, CurrencyCode = b.CurrencyCode, AccountNumberIban = b.AccountNumberIban, BankCode = b.BankCode, Swift = b.Swift, TransitAmount = b.TransitAmount, CorrespondentBankId = b.CorrespondentBankId, CorrespondentAccount = b.CorrespondentAccount });
        if (d.Signatures is { Count: > 0 })
            foreach (var s in d.Signatures)
                entity.Signatures.Add(new CompanySignature { Id = Guid.NewGuid(), CompanyId = entity.Id, Type = s.Type, FileName = s.FileName, FilePath = s.FilePath, SignatoryName = s.SignatoryName, Role = s.Role });
        await repository.AddAsync(entity, ct);
        var loaded = await repository.GetByIdAsync(entity.Id, ct);
        return CompanyMapping.MapToDto(loaded!);
    }
}

public sealed record UpdateCompanyCommand(Guid Id, UpdateCompanyDto Dto) : IRequest<CompanyDto?>;
public sealed class UpdateCompanyCommandHandler(ICompanyRepository repository) : IRequestHandler<UpdateCompanyCommand, CompanyDto?>
{
    public async Task<CompanyDto?> Handle(UpdateCompanyCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        var d = request.Dto;
        entity.Name = d.Name;
        entity.NameFull = d.NameFull;
        entity.DirectorsFullName = d.DirectorsFullName;
        entity.InTheNameOfWhom = d.InTheNameOfWhom;
        entity.WorkerPostId = d.WorkerPostId;
        entity.Post = d.Post;
        entity.VatRate = d.VatRate;
        entity.PricingTypeId = d.PricingTypeId;
        entity.PricingType = d.PricingType;
        entity.CompanyPrefix = d.CompanyPrefix;
        entity.CompanyCodeType = d.CompanyCodeType;
        entity.CompanyCode = d.CompanyCode;
        entity.VatCode = d.VatCode;
        entity.Rrc = d.Rrc;
        entity.CorrespondentAccount = d.CorrespondentAccount;
        entity.Okpo = d.Okpo;
        entity.Ogrn = d.Ogrn;
        entity.CountryId = d.CountryId;
        entity.StateId = d.StateId;
        entity.CityId = d.CityId;
        entity.Address = d.Address;
        entity.PostCode = d.PostCode;
        entity.Telephone = d.Telephone;
        entity.Fax = d.Fax;
        entity.Email = d.Email;
        entity.Website = d.Website;
        entity.IsMainCompany = d.IsMainCompany;
        entity.CorrespondentCountryId = d.CorrespondentCountryId;
        entity.CorrespondentStateId = d.CorrespondentStateId;
        entity.CorrespondentCityId = d.CorrespondentCityId;
        entity.CorrespondentAddress = d.CorrespondentAddress;
        entity.CorrespondentPostCode = d.CorrespondentPostCode;
        entity.CorrespondentTelephone = d.CorrespondentTelephone;
        entity.CorrespondentFax = d.CorrespondentFax;
        entity.CorrespondentEmail = d.CorrespondentEmail;
        entity.CorrespondentWebsite = d.CorrespondentWebsite;
        entity.IsActive = d.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        entity.BankAccounts.Clear();
        entity.Signatures.Clear();
        if (d.BankAccounts is { Count: > 0 })
            foreach (var b in d.BankAccounts)
                entity.BankAccounts.Add(new CompanyBankAccount { Id = Guid.NewGuid(), CompanyId = entity.Id, BankId = b.BankId, CurrencyCode = b.CurrencyCode, AccountNumberIban = b.AccountNumberIban, BankCode = b.BankCode, Swift = b.Swift, TransitAmount = b.TransitAmount, CorrespondentBankId = b.CorrespondentBankId, CorrespondentAccount = b.CorrespondentAccount });
        if (d.Signatures is { Count: > 0 })
            foreach (var s in d.Signatures)
                entity.Signatures.Add(new CompanySignature { Id = Guid.NewGuid(), CompanyId = entity.Id, Type = s.Type, FileName = s.FileName, FilePath = s.FilePath, SignatoryName = s.SignatoryName, Role = s.Role });

        await repository.UpdateWithChildrenAsync(entity, ct);
        var loaded = await repository.GetByIdAsync(entity.Id, ct);
        return loaded == null ? null : CompanyMapping.MapToDto(loaded);
    }
}

public sealed record DeleteCompanyCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteCompanyCommandHandler(ICompanyRepository repository) : IRequestHandler<DeleteCompanyCommand, bool>
{
    public async Task<bool> Handle(DeleteCompanyCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}

internal static class CompanyMapping
{
    public static CompanyDto MapToDto(Company e) => new()
    {
        Id = e.Id,
        Name = e.Name,
        NameFull = e.NameFull,
        DirectorsFullName = e.DirectorsFullName,
        InTheNameOfWhom = e.InTheNameOfWhom,
        WorkerPostId = e.WorkerPostId,
        Post = e.Post ?? e.WorkerPost?.Name,
        VatRate = e.VatRate,
        PricingTypeId = e.PricingTypeId,
        PricingType = e.PricingType ?? e.PricingTypeEntity?.Name,
        CompanyPrefix = e.CompanyPrefix,
        CompanyCodeType = e.CompanyCodeType,
        CompanyCode = e.CompanyCode,
        VatCode = e.VatCode,
        Rrc = e.Rrc,
        CorrespondentAccount = e.CorrespondentAccount,
        Okpo = e.Okpo,
        Ogrn = e.Ogrn,
        CountryId = e.CountryId,
        StateId = e.StateId,
        CityId = e.CityId,
        Address = e.Address,
        PostCode = e.PostCode,
        Telephone = e.Telephone,
        Fax = e.Fax,
        Email = e.Email,
        Website = e.Website,
        IsMainCompany = e.IsMainCompany,
        CorrespondentCountryId = e.CorrespondentCountryId,
        CorrespondentStateId = e.CorrespondentStateId,
        CorrespondentCityId = e.CorrespondentCityId,
        CorrespondentAddress = e.CorrespondentAddress,
        CorrespondentPostCode = e.CorrespondentPostCode,
        CorrespondentTelephone = e.CorrespondentTelephone,
        CorrespondentFax = e.CorrespondentFax,
        CorrespondentEmail = e.CorrespondentEmail,
        CorrespondentWebsite = e.CorrespondentWebsite,
        IsActive = e.IsActive,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt,
        BankAccounts = e.BankAccounts.Select(b => new CompanyBankAccountDto(b.Id, b.BankId, b.CurrencyCode, b.AccountNumberIban, b.BankCode, b.Swift, b.TransitAmount, b.CorrespondentBankId, b.CorrespondentAccount)).ToList(),
        Signatures = e.Signatures.Select(s => new CompanySignatureDto(s.Id, s.Type ?? "Signature", s.FileName, s.FilePath, s.SignatoryName, s.Role)).ToList()
    };
}

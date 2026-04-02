namespace Settings.Application.DTOs.Company;

public record CompanyDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? NameFull { get; init; }
    public string? DirectorsFullName { get; init; }
    public string? InTheNameOfWhom { get; init; }
    public Guid? WorkerPostId { get; init; }
    public string? Post { get; init; }
    public string? VatRate { get; init; }
    public Guid? PricingTypeId { get; init; }
    public string? PricingType { get; init; }
    public string? CompanyPrefix { get; init; }
    public string? CompanyCodeType { get; init; }
    public string? CompanyCode { get; init; }
    public string? VatCode { get; init; }
    public string? Rrc { get; init; }
    public string? CorrespondentAccount { get; init; }
    public string? Okpo { get; init; }
    public string? Ogrn { get; init; }
    public Guid? CountryId { get; init; }
    public Guid? StateId { get; init; }
    public Guid? CityId { get; init; }
    public string? Address { get; init; }
    public string? PostCode { get; init; }
    public string? Telephone { get; init; }
    public string? Fax { get; init; }
    public string? Email { get; init; }
    public string? Website { get; init; }
    public bool IsMainCompany { get; init; }
    public Guid? CorrespondentCountryId { get; init; }
    public Guid? CorrespondentStateId { get; init; }
    public Guid? CorrespondentCityId { get; init; }
    public string? CorrespondentAddress { get; init; }
    public string? CorrespondentPostCode { get; init; }
    public string? CorrespondentTelephone { get; init; }
    public string? CorrespondentFax { get; init; }
    public string? CorrespondentEmail { get; init; }
    public string? CorrespondentWebsite { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public IReadOnlyList<CompanyBankAccountDto> BankAccounts { get; init; } = [];
    public IReadOnlyList<CompanySignatureDto> Signatures { get; init; } = [];
}

public record CompanyBankAccountDto(Guid Id, Guid? BankId, string? CurrencyCode, string? AccountNumberIban, string? BankCode, string? Swift, string? TransitAmount, Guid? CorrespondentBankId, string? CorrespondentAccount);
public record CompanySignatureDto(Guid Id, string? Type, string? FileName, string? FilePath, string? SignatoryName, string? Role);

public record CreateCompanyBankAccountDto(Guid? BankId, string? CurrencyCode, string? AccountNumberIban, string? BankCode, string? Swift, string? TransitAmount, Guid? CorrespondentBankId, string? CorrespondentAccount);
public record CreateCompanySignatureDto(string Type, string? FileName, string? FilePath, string? SignatoryName = null, string? Role = null);

namespace Clients.Application.DTOs.Client;

public record CreateClientDto
{
    public bool IsDeactive { get; init; }

    public ClientGeneralInformationDto General { get; init; } = new();
    public ClientLegalAddressDto Legal { get; init; } = new();
    public ClientPostalInformationDto Postal { get; init; } = new();
    public ClientPaymentInformationDto Payment { get; init; } = new();

    public IReadOnlyList<CreateClientContactPersonDto> ContactPersons { get; init; } = [];
    public IReadOnlyList<CreateClientBankAccountDto> BankAccounts { get; init; } = [];
}

public record CreateClientContactPersonDto
{
    public string? EnglishName { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public string? Mobile { get; init; }
    public string? Fax { get; init; }
    public Guid? WorkerPostId { get; init; }
}

public record CreateClientBankAccountDto
{
    public Guid? BankId { get; init; }
    public Guid? CurrencyId { get; init; }
    public string? AccountNumberIban { get; init; }
    public string? TransitAmount { get; init; }
    public Guid? CorrespondentBankId { get; init; }
    public string? CorrespondentAccount { get; init; }
}

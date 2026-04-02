namespace Clients.Application.DTOs.Client;

public record ClientDto
{
    public Guid Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public bool IsDeactive { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public ClientGeneralInformationDto General { get; init; } = new();
    public ClientLegalAddressDto Legal { get; init; } = new();
    public ClientPostalInformationDto Postal { get; init; } = new();
    public ClientPaymentInformationDto Payment { get; init; } = new();

    public IReadOnlyList<ClientContactPersonDto> ContactPersons { get; init; } = [];
    public IReadOnlyList<ClientBankAccountDto> BankAccounts { get; init; } = [];
}

public record ClientContactPersonDto
{
    public Guid Id { get; init; }
    public string? EnglishName { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public string? Mobile { get; init; }
    public string? Fax { get; init; }
    public Guid? WorkerPostId { get; init; }
}

public record ClientBankAccountDto
{
    public Guid Id { get; init; }
    public Guid? BankId { get; init; }
    public Guid? CurrencyId { get; init; }
    public string? AccountNumberIban { get; init; }
    public string? TransitAmount { get; init; }
    public Guid? CorrespondentBankId { get; init; }
    public string? CorrespondentAccount { get; init; }

    /// <summary>Populated from Settings when returning the client (not sent on create/update).</summary>
    public ClientBankDetailsDto? BankDetails { get; init; }

    /// <summary>Populated from Settings when returning the client (not sent on create/update).</summary>
    public ClientBankDetailsDto? CorrespondentBankDetails { get; init; }
}

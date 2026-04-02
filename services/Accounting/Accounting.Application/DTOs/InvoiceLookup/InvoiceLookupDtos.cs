namespace Accounting.Application.DTOs.InvoiceLookup;

public record InvoiceLookupOptionDto(string Category, string Code, string Name, int SortOrder);

public record CreateInvoiceLookupOptionDto(string Category, string Code, string Name);

public record CreateInvoiceLookupOutcome(bool Success, string? Error, InvoiceLookupOptionDto? Data);

namespace Settings.Application.DTOs.QuoteSource;

public record CreateQuoteSourceDto(string Name, int DisplayOrder, bool IsActive);

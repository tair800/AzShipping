namespace Settings.Application.DTOs.QuoteSource;

public record QuoteSourceDto(Guid Id, string Name, int DisplayOrder, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);

namespace Request.Application.DTOs.SaleStatus;

public record SaleStatusDto(Guid Id, string Name, int SortOrder, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);

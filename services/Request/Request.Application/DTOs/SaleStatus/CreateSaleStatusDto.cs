namespace Request.Application.DTOs.SaleStatus;

public record CreateSaleStatusDto(string Name, int SortOrder = 0, bool IsActive = true);

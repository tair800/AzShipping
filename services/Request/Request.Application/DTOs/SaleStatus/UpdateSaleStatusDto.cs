namespace Request.Application.DTOs.SaleStatus;

public record UpdateSaleStatusDto(string Name, int SortOrder, bool IsActive = true);

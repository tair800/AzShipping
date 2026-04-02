namespace Settings.Application.DTOs.SalesFunnelStatus;

public record CreateSalesFunnelStatusDto(string Name, int StatusPosition, Guid? ResponsibleManagerId, int NumberOfDays, bool SendToEmail, bool SendNotification, bool IsActive);

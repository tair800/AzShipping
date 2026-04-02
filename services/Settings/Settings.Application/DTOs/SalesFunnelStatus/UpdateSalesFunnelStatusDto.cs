namespace Settings.Application.DTOs.SalesFunnelStatus;

public record UpdateSalesFunnelStatusDto(string Name, int StatusPosition, Guid? ResponsibleManagerId, int NumberOfDays, bool SendToEmail, bool SendNotification, bool IsActive);

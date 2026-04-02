namespace Settings.Application.DTOs.ClientSource;

public record ClientSourceDto(Guid Id, string Name, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
public record CreateClientSourceDto(string Name, bool IsActive);
public record UpdateClientSourceDto(string Name, bool IsActive);

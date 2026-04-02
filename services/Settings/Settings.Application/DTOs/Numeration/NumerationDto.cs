namespace Settings.Application.DTOs.Numeration;

public record NumerationDto(
    Guid Id,
    string Name,
    string NumerationForCode,
    Guid? CompanyId,
    string? CompanyName,
    Guid? DepartmentId,
    string? DepartmentName,
    Guid? EmployeeId,
    Guid? ClientId,
    string? ElementCode,
    string? DocumentTypeCode,
    int NumberOfDigits,
    int CurrentIndex,
    string Formula,
    bool IsSystemic,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

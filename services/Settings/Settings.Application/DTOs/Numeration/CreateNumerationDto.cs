namespace Settings.Application.DTOs.Numeration;

public record CreateNumerationDto(
    string Name,
    string NumerationForCode,
    Guid? CompanyId,
    Guid? DepartmentId,
    Guid? EmployeeId,
    Guid? ClientId,
    string? ElementCode,
    string? DocumentTypeCode,
    int NumberOfDigits,
    int CurrentIndex,
    string Formula,
    bool IsSystemic = false);

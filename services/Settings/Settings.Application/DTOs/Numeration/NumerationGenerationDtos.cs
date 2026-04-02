namespace Settings.Application.DTOs.Numeration;

public sealed record NumerationGenerateRequestDto(
    string NumerationForCode,
    Guid? CompanyId,
    Guid? DepartmentId,
    Guid? ClientId,
    Guid? EmployeeId,
    string? ElementCode,
    string? DocumentTypeCode,
    string? CompanyCode,
    string? CompanyPrefix,
    string? DepartmentCode,
    string? DepartmentPrefix,
    string? ClientCode,
    string? EmployeeCode,
    DateTime? Date,
    Dictionary<string, string>? Tokens);

public sealed record NumerationGenerateResponseDto(
    Guid NumerationId,
    string NumerationName,
    string Value,
    int Index,
    int SpecificityScore,
    bool IsSystemicFallback,
    string Formula);

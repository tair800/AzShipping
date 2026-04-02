using Settings.Domain.AggregatesModel.CompanyAggregate;
using Settings.Domain.AggregatesModel.DepartmentAggregate;

namespace Settings.Domain.AggregatesModel.NumerationAggregate;

public class Numeration
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    /// <summary>Code from NumerationForTypeOptions (e.g. ForRequest, ForOrder, IssuedInvoices).</summary>
    public string NumerationForCode { get; set; } = string.Empty;
    public Guid? CompanyId { get; set; }
    public Guid? DepartmentId { get; set; }
    /// <summary>Cross-service reference to Employee/User. No FK in Settings.</summary>
    public Guid? EmployeeId { get; set; }
    /// <summary>Cross-service reference to Client. No FK in Settings.</summary>
    public Guid? ClientId { get; set; }
    /// <summary>Optional business element discriminator (e.g. SEA, AIR, CARGO, TRIP).</summary>
    public string? ElementCode { get; set; }
    /// <summary>Optional document type discriminator (e.g. INV, ORD, ACT).</summary>
    public string? DocumentTypeCode { get; set; }
    public int NumberOfDigits { get; set; } = 3;
    public int CurrentIndex { get; set; } = 0;
    public string Formula { get; set; } = string.Empty;
    public bool IsSystemic { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Company? Company { get; set; }
    public Department? Department { get; set; }
}

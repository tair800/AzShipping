using Settings.Domain.AggregatesModel.CompanyAggregate;

namespace Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

/// <summary>ERP “group of employees” (UI): name, optional company, module permission matrix as JSON. Not the same as Identity auth roles.</summary>
public class EmployeeGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }

    /// <summary>
    /// Per-module permissions (tabs: Request, Orders, Clients, …). Shape is defined by the client (checkboxes, dropdowns).
    /// Stored as JSON object, default <c>{}</c>.
    /// </summary>
    public string PermissionsJson { get; set; } = "{}";

    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
}

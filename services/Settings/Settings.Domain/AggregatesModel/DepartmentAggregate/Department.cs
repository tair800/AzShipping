using Settings.Domain.AggregatesModel.CompanyAggregate;

namespace Settings.Domain.AggregatesModel.DepartmentAggregate;

public class Department
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Prefix { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Company? Company { get; set; }
}

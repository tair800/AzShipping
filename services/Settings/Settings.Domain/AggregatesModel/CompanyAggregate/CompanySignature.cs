namespace Settings.Domain.AggregatesModel.CompanyAggregate;

public class CompanySignature
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    /// <summary>Type: Seal, Logo, or Signature</summary>
    public string? Type { get; set; }
    public string? FileName { get; set; }
    public string? FilePath { get; set; }
    public string? SignatoryName { get; set; }
    public string? Role { get; set; }
}

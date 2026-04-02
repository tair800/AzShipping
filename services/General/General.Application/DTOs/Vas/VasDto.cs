namespace General.Application.DTOs.Vas;

public class VasDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public decimal? OverWidth { get; set; }
    public decimal? OverHeight { get; set; }
    public decimal? OverWeight { get; set; }
    public bool IsMandatory { get; set; }
    public string? ExecutionPlace { get; set; }
    public string? Uom { get; set; }
    public bool IsAir { get; set; }
    public bool IsSea { get; set; }
    public bool IsRoad { get; set; }
    public bool IsRail { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public decimal? Amount { get; set; }
    public Guid? CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
}

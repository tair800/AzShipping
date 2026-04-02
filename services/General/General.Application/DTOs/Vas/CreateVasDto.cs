namespace General.Application.DTOs.Vas;

public class CreateVasDto
{
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
    public bool IsActive { get; set; } = true;
    public decimal? Amount { get; set; }
    public Guid? CurrencyId { get; set; }
}

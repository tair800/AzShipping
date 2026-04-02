using General.Domain.AggregatesModel.CurrencyAggregate;

namespace General.Domain.AggregatesModel.VasAggregate;

public class Vas
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
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; }

    /// <summary>Amount for this VAS (optional)</summary>
    public decimal? Amount { get; set; }
    /// <summary>Currency for the amount</summary>
    public Guid? CurrencyId { get; set; }
    public Currency? Currency { get; set; }
}

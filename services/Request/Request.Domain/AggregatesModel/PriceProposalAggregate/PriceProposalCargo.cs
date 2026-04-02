namespace Request.Domain.AggregatesModel.PriceProposalAggregate;

public class PriceProposalCargo
{
    public Guid Id { get; set; }
    public Guid PriceProposalId { get; set; }
    public string? Description { get; set; }
    public int? Quantity { get; set; }
    public string? PackageType { get; set; }
    public bool IncludeInsurance { get; set; }
    public string? DescriptionOfGoods { get; set; }
}

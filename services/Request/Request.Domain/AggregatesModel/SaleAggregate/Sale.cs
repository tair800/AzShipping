using Request.Domain.AggregatesModel.SaleStatusAggregate;

namespace Request.Domain.AggregatesModel.SaleAggregate;

public class Sale
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string RequestNumber { get; set; } = string.Empty;
    public bool HasSea { get; set; }
    public bool HasAir { get; set; }
    public bool HasRail { get; set; }
    public bool HasRoad { get; set; }
    public Guid? ClientId { get; set; }
    public string? ClientName { get; set; }
    public string? SubType { get; set; }
    public Guid? CarrierId { get; set; }
    public string? CarrierName { get; set; }
    public Guid? SaleStatusId { get; set; }
    public SaleStatus? SaleStatus { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? CargoName { get; set; }
    public decimal? CargoVolume { get; set; }
    public decimal? CargoWeight { get; set; }
    public string? CargoSize { get; set; }
    public string? LoadingPlace { get; set; }
    public string? UnloadingPlace { get; set; }
    public decimal? DealValue { get; set; }
    public string? DealValueCurrency { get; set; }
    public string? ManagerSellerName { get; set; }
    public string? PriceProposal { get; set; }
    public string SaleListStatus { get; set; } = "Active"; // Active, Deactive, Cancelled, Converted
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

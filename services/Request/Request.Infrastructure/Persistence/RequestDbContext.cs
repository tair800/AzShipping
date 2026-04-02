using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.CommercialOfferAggregate;
using Request.Domain.AggregatesModel.PriceProposalAggregate;
using Request.Domain.AggregatesModel.RequestAggregate;
using Request.Domain.AggregatesModel.RequestCommentAggregate;
using Request.Domain.AggregatesModel.RequestNegotiationAggregate;
using Request.Domain.AggregatesModel.SaleAggregate;
using Request.Domain.AggregatesModel.SaleStatusAggregate;

namespace Request.Infrastructure.Persistence;

public class RequestDbContext : DbContext
{
    public RequestDbContext(DbContextOptions<RequestDbContext> options) : base(options) { }

    public DbSet<SaleStatus> SaleStatuses { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<RequestNegotiation> RequestNegotiations { get; set; }
    public DbSet<RequestType> RequestTypes { get; set; }
    public DbSet<RequestEntity> Requests { get; set; }
    public DbSet<RequestDimension> RequestDimensions { get; set; }
    public DbSet<RequestVas> RequestVas { get; set; }
    public DbSet<RequestComment> RequestComments { get; set; }
    public DbSet<PriceProposal> PriceProposals { get; set; }
    public DbSet<PriceProposalCargo> PriceProposalCargos { get; set; }
    public DbSet<CommercialOffer> CommercialOffers { get; set; }
    public DbSet<CommercialOfferSelectedProposal> CommercialOfferSelectedProposals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<SaleStatus>(e =>
        {
            e.ToTable("SaleStatuses");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
        });
        modelBuilder.Entity<Sale>(e =>
        {
            e.ToTable("Sales");
            e.HasKey(x => x.Id);
            e.Property(x => x.RequestNumber).IsRequired().HasMaxLength(100);
            e.Property(x => x.ClientName).HasMaxLength(300);
            e.Property(x => x.SubType).HasMaxLength(100);
            e.Property(x => x.CarrierName).HasMaxLength(300);
            e.Property(x => x.CargoName).HasMaxLength(300);
            e.Property(x => x.CargoSize).HasMaxLength(50);
            e.Property(x => x.LoadingPlace).HasMaxLength(300);
            e.Property(x => x.UnloadingPlace).HasMaxLength(300);
            e.Property(x => x.DealValueCurrency).HasMaxLength(10);
            e.Property(x => x.ManagerSellerName).HasMaxLength(300);
            e.Property(x => x.PriceProposal).HasMaxLength(1000);
            e.Property(x => x.SaleListStatus).HasMaxLength(50);
            e.HasOne(x => x.SaleStatus).WithMany().HasForeignKey(x => x.SaleStatusId).OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<RequestNegotiation>(e =>
        {
            e.ToTable("RequestNegotiations");
            e.HasKey(x => x.Id);
            e.Property(x => x.ClientName).HasMaxLength(300);
            e.Property(x => x.WayOfNegotiationName).HasMaxLength(200);
            e.Property(x => x.Question).HasMaxLength(2000);
            e.Property(x => x.Answer).HasMaxLength(2000);
            e.Property(x => x.Result).HasMaxLength(2000);
        });
        modelBuilder.Entity<RequestType>(e =>
        {
            e.ToTable("RequestTypes");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).IsRequired().HasMaxLength(50);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Direction).HasMaxLength(50);
            e.Property(x => x.Mode).HasMaxLength(50);
            e.Property(x => x.SubType).HasMaxLength(50);
            e.Property(x => x.RequestNumberPrefix).HasMaxLength(20);
            e.Property(x => x.CarrierApiPath).HasMaxLength(100);
            e.Property(x => x.CarrierLabel).HasMaxLength(100);
        });
        modelBuilder.Entity<RequestEntity>(e =>
        {
            e.ToTable("Requests");
            e.HasKey(x => x.Id);
            e.Property(x => x.RequestNumber).IsRequired().HasMaxLength(100);
            e.Property(x => x.CompanyName).HasMaxLength(300);
            e.Property(x => x.ManagerName).HasMaxLength(200);
            e.Property(x => x.LogisticianName).HasMaxLength(200);
            e.Property(x => x.DepartmentName).HasMaxLength(200);
            e.Property(x => x.ShipperName).HasMaxLength(300);
            e.Property(x => x.ConsigneeName).HasMaxLength(300);
            e.Property(x => x.MyCustomerTypeName).HasMaxLength(200);
            e.Property(x => x.StatusName).HasMaxLength(100);
            e.Property(x => x.CurrencyCode).HasMaxLength(10);
            e.Property(x => x.VatRate).HasMaxLength(100);
            e.Property(x => x.SourceOfRequestName).HasMaxLength(200);
            e.Property(x => x.RequestPurposeName).HasMaxLength(200);
            e.Property(x => x.GatewayName).HasMaxLength(200);
            e.Property(x => x.ViaPortName).HasMaxLength(200);
            e.Property(x => x.DestinationName).HasMaxLength(200);
            e.Property(x => x.ViaPort2Name).HasMaxLength(200);
            e.Property(x => x.TransitPortName).HasMaxLength(200);
            e.Property(x => x.CarrierName).HasMaxLength(200);
            e.Property(x => x.StationOfDeliveryName).HasMaxLength(200);
            e.Property(x => x.DescriptionOfGoods).HasMaxLength(2000);
            e.Property(x => x.Notes).HasMaxLength(4000);
            e.HasOne<RequestType>().WithMany().HasForeignKey(x => x.RequestTypeId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<RequestDimension>(e =>
        {
            e.ToTable("RequestDimensions");
            e.HasKey(x => x.Id);
            e.Property(x => x.PackageType).HasMaxLength(50);
            e.HasOne<RequestEntity>().WithMany().HasForeignKey(x => x.RequestId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<RequestVas>(e =>
        {
            e.ToTable("RequestVas");
            e.HasKey(x => x.Id);
            e.Property(x => x.VasName).HasMaxLength(200);
            e.Property(x => x.ExecutionPlace).HasMaxLength(200);
            e.Property(x => x.Uom).HasMaxLength(100);
            e.Property(x => x.CurrencyCode).HasMaxLength(10);
            e.Property(x => x.Notes).HasMaxLength(1000);
            e.HasOne<RequestEntity>().WithMany().HasForeignKey(x => x.RequestId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<RequestComment>(e =>
        {
            e.ToTable("RequestComments");
            e.HasKey(x => x.Id);
            e.Property(x => x.Comments).HasMaxLength(4000);
            e.HasOne<RequestEntity>().WithMany().HasForeignKey(x => x.RequestId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<PriceProposal>(e =>
        {
            e.ToTable("PriceProposals");
            e.HasKey(x => x.Id);
            e.Property(x => x.Type).HasMaxLength(50);
            e.Property(x => x.TemplateName).HasMaxLength(200);
            e.Property(x => x.CarrierName).HasMaxLength(300);
            e.Property(x => x.TypeOfService).HasMaxLength(200);
            e.Property(x => x.Name).IsRequired().HasMaxLength(300);
            e.Property(x => x.ClientVatRateCode).HasMaxLength(50);
            e.Property(x => x.ClientCurrencyCode).HasMaxLength(10);
            e.Property(x => x.CarrierVatRateCode).HasMaxLength(50);
            e.Property(x => x.CarrierCurrencyCode).HasMaxLength(10);
            e.Property(x => x.Route).HasMaxLength(500);
            e.Property(x => x.Comments).HasMaxLength(2000);
            e.Property(x => x.UserName).HasMaxLength(200);
            e.HasOne<RequestEntity>().WithMany().HasForeignKey(x => x.RequestId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<PriceProposalCargo>(e =>
        {
            e.ToTable("PriceProposalCargos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Description).HasMaxLength(200);
            e.Property(x => x.PackageType).HasMaxLength(100);
            e.Property(x => x.DescriptionOfGoods).HasMaxLength(1000);
            e.HasOne<PriceProposal>().WithMany(x => x.CargoItems).HasForeignKey(x => x.PriceProposalId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<CommercialOffer>(e =>
        {
            e.ToTable("CommercialOffers");
            e.HasKey(x => x.Id);
            e.Property(x => x.TemplateName).HasMaxLength(300);
            e.Property(x => x.DocumentName).IsRequired().HasMaxLength(500);
            e.Property(x => x.DocumentSourceType).IsRequired().HasMaxLength(50);
            e.Property(x => x.AttachedFileReference).HasMaxLength(1000);
            e.Property(x => x.Comments).HasMaxLength(4000);
            e.Property(x => x.UserName).HasMaxLength(200);
            e.HasOne<RequestEntity>().WithMany().HasForeignKey(x => x.RequestId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.SelectedProposals).WithOne(x => x.CommercialOffer).HasForeignKey(x => x.CommercialOfferId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<CommercialOfferSelectedProposal>(e =>
        {
            e.ToTable("CommercialOfferSelectedProposals");
            e.HasKey(x => x.Id);
            e.HasIndex(x => new { x.CommercialOfferId, x.PriceProposalId }).IsUnique();
            e.HasOne(x => x.PriceProposal).WithMany().HasForeignKey(x => x.PriceProposalId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}

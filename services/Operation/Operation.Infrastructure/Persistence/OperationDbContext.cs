using Microsoft.EntityFrameworkCore;
using Operation.Domain.AggregatesModel.OperationAggregate;

namespace Operation.Infrastructure.Persistence;

public class OperationDbContext : DbContext
{
    public OperationDbContext(DbContextOptions<OperationDbContext> options) : base(options) { }

    public DbSet<OperationType> OperationTypes { get; set; }
    public DbSet<LogisticsOperation> Operations { get; set; }
    public DbSet<OperationDimension> OperationDimensions { get; set; }
    public DbSet<OperationPackageLine> OperationPackageLines { get; set; }
    public DbSet<OperationVas> OperationVas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OperationType>(e =>
        {
            e.ToTable("OperationTypes");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).IsRequired().HasMaxLength(50);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Direction).HasMaxLength(50);
            e.Property(x => x.Mode).HasMaxLength(50);
            e.Property(x => x.SubType).HasMaxLength(50);
            e.Property(x => x.OperationNumberPrefix).HasMaxLength(20);
            e.Property(x => x.CarrierApiPath).HasMaxLength(100);
            e.Property(x => x.CarrierLabel).HasMaxLength(100);
        });

        modelBuilder.Entity<LogisticsOperation>(e =>
        {
            e.ToTable("Operations");
            e.HasKey(x => x.Id);
            e.Property(x => x.OperationNumber).IsRequired().HasMaxLength(100);
            e.Property(x => x.ModalType).IsRequired().HasMaxLength(50);
            e.Property(x => x.PricingMode).IsRequired().HasMaxLength(50);
            e.Property(x => x.ClientOrderNumber).HasMaxLength(100);
            e.Property(x => x.CompanyName).HasMaxLength(300);
            e.Property(x => x.ManagerName).HasMaxLength(200);
            e.Property(x => x.LogisticianName).HasMaxLength(200);
            e.Property(x => x.DepartmentName).HasMaxLength(200);
            e.Property(x => x.ShipperName).HasMaxLength(300);
            e.Property(x => x.ConsigneeName).HasMaxLength(300);
            e.Property(x => x.MyCustomerParty).HasMaxLength(50);
            e.Property(x => x.MyCustomerName).HasMaxLength(300);
            e.Property(x => x.StartTime).HasMaxLength(16);
            e.Property(x => x.IncotermName).HasMaxLength(100);
            e.Property(x => x.FreightPrepaidCollect).HasMaxLength(50);
            e.Property(x => x.MoveTypeName).HasMaxLength(100);
            e.Property(x => x.OtherPrepaidCollect).HasMaxLength(50);
            e.Property(x => x.SalesmanName).HasMaxLength(200);
            e.Property(x => x.CurrencyCode).HasMaxLength(10);
            e.Property(x => x.VatRate).HasMaxLength(100);
            e.Property(x => x.DeferredPaymentConditionName).HasMaxLength(200);
            e.Property(x => x.PickupCountryName).HasMaxLength(200);
            e.Property(x => x.PickupStateName).HasMaxLength(200);
            e.Property(x => x.PickupCityName).HasMaxLength(200);
            e.Property(x => x.PickupZipCode).HasMaxLength(50);
            e.Property(x => x.GatewayName).HasMaxLength(200);
            e.Property(x => x.ViaPortName).HasMaxLength(200);
            e.Property(x => x.DestinationName).HasMaxLength(200);
            e.Property(x => x.ViaPort2Name).HasMaxLength(200);
            e.Property(x => x.CarrierName).HasMaxLength(200);
            e.Property(x => x.FlightNumber).HasMaxLength(100);
            e.Property(x => x.Mawb).HasMaxLength(100);
            e.Property(x => x.PortOfDeliveryName).HasMaxLength(200);
            e.Property(x => x.OceanBillOfLading).HasMaxLength(100);
            e.Property(x => x.VesselName).HasMaxLength(200);
            e.Property(x => x.RoadTruckerNumber).HasMaxLength(100);
            e.Property(x => x.RoadWaybillNumber).HasMaxLength(100);
            e.Property(x => x.DeliveryCountryName).HasMaxLength(200);
            e.Property(x => x.DeliveryStateName).HasMaxLength(200);
            e.Property(x => x.DeliveryCityName).HasMaxLength(200);
            e.Property(x => x.DeliveryZipCode).HasMaxLength(50);
            e.Property(x => x.DescriptionOfGoods).HasMaxLength(2000);
            e.Property(x => x.AgentName).HasMaxLength(300);
            e.Property(x => x.Reference1).HasMaxLength(200);
            e.Property(x => x.Reference2).HasMaxLength(200);
            e.Property(x => x.MainHarmonize).HasMaxLength(200);
            e.Property(x => x.NotesToBePrinted).HasMaxLength(2000);
            e.Property(x => x.TrackingNumber).HasMaxLength(200);
            e.Property(x => x.TemplateName).HasMaxLength(200);
            e.Property(x => x.Notes).HasMaxLength(4000);
            e.Property(x => x.OperationStageName).HasMaxLength(100);
            e.Property(x => x.CargoName).HasMaxLength(300);
            e.Property(x => x.LoadingMethodName).HasMaxLength(200);
            e.Property(x => x.CargoTransportTypeName).HasMaxLength(200);
            e.Property(x => x.ConsignmentCurrencyCode).HasMaxLength(10);
            e.Property(x => x.CargoAdditionalInformation).HasMaxLength(2000);
            e.HasOne<OperationType>().WithMany().HasForeignKey(x => x.OperationTypeId).OnDelete(DeleteBehavior.Restrict);
            e.HasMany(x => x.Dimensions).WithOne(x => x.Operation).HasForeignKey(x => x.OperationId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.PackageLines).WithOne(x => x.Operation).HasForeignKey(x => x.OperationId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.VasItems).WithOne(x => x.Operation).HasForeignKey(x => x.OperationId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OperationVas>(e =>
        {
            e.ToTable("OperationVas");
            e.HasKey(x => x.Id);
            e.Property(x => x.VasName).HasMaxLength(200);
            e.Property(x => x.ExecutionPlace).HasMaxLength(200);
            e.Property(x => x.Uom).HasMaxLength(50);
            e.Property(x => x.CurrencyCode).HasMaxLength(10);
            e.Property(x => x.Notes).HasMaxLength(2000);
        });

        modelBuilder.Entity<OperationDimension>(e =>
        {
            e.ToTable("OperationDimensions");
            e.HasKey(x => x.Id);
            e.Property(x => x.PackageType).HasMaxLength(200);
        });

        modelBuilder.Entity<OperationPackageLine>(e =>
        {
            e.ToTable("OperationPackageLines");
            e.HasKey(x => x.Id);
            e.Property(x => x.PackageType).HasMaxLength(200);
        });
    }
}

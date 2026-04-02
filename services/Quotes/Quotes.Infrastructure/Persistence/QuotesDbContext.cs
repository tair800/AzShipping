using Microsoft.EntityFrameworkCore;
using Quotes.Domain.AggregatesModel.AddressAggregate;
using Quotes.Domain.AggregatesModel.QuoteAggregate;

namespace Quotes.Infrastructure.Persistence;

public class QuotesDbContext : DbContext
{
    public QuotesDbContext(DbContextOptions<QuotesDbContext> options) : base(options) { }

    public DbSet<QuoteType> QuoteTypes { get; set; }
    public DbSet<QuoteEntity> Quotes { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<QuoteType>(e =>
        {
            e.ToTable("QuoteTypes");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).IsRequired().HasMaxLength(50);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Direction).HasMaxLength(50);
            e.Property(x => x.Mode).HasMaxLength(50);
            e.Property(x => x.SubType).HasMaxLength(50);
            e.Property(x => x.QuoteNumberPrefix).HasMaxLength(20);
            e.Property(x => x.CarrierApiPath).HasMaxLength(100);
            e.Property(x => x.CarrierLabel).HasMaxLength(100);
        });

        modelBuilder.Entity<QuoteEntity>(e =>
        {
            e.ToTable("Quotes");
            e.HasKey(x => x.Id);
            e.Property(x => x.QuoteNumber).IsRequired().HasMaxLength(100);
            e.Property(x => x.CompanyName).HasMaxLength(300);
            e.Property(x => x.ManagerName).HasMaxLength(200);
            e.Property(x => x.LogisticianName).HasMaxLength(200);
            e.Property(x => x.HandlerName).HasMaxLength(200);
            e.Property(x => x.AccountManagerName).HasMaxLength(200);
            e.Property(x => x.OpenedByName).HasMaxLength(200);
            e.Property(x => x.ManagerUserId);
            e.Property(x => x.HandlerUserId);
            e.Property(x => x.AccountManagerUserId);
            e.Property(x => x.OpenedByUserId);
            e.Property(x => x.DepartmentName).HasMaxLength(200);
            e.Property(x => x.QuoteStatus).HasMaxLength(100);
            e.Property(x => x.ShipperName).HasMaxLength(300);
            e.Property(x => x.ConsigneeName).HasMaxLength(300);
            e.Property(x => x.MyCustomerTypeName).HasMaxLength(200);
            e.Property(x => x.RateType).HasMaxLength(50);
            e.Property(x => x.IncotermName).HasMaxLength(100);
            e.Property(x => x.MoveTypeName).HasMaxLength(100);
            e.Property(x => x.CurrencyCode).HasMaxLength(10);
            e.Property(x => x.MinVat).HasMaxLength(100);
            e.Property(x => x.VatRate).HasMaxLength(100);
            e.Property(x => x.VatNote).HasMaxLength(2000);
            e.Property(x => x.PickupCountryName).HasMaxLength(200);
            e.Property(x => x.PickupStateName).HasMaxLength(200);
            e.Property(x => x.PickupCityName).HasMaxLength(200);
            e.Property(x => x.PickupZipCode).HasMaxLength(50);
            e.Property(x => x.GatewayName).HasMaxLength(200);
            e.Property(x => x.ViaPortName).HasMaxLength(200);
            e.Property(x => x.DestinationName).HasMaxLength(200);
            e.Property(x => x.ViaPort2Name).HasMaxLength(200);
            e.Property(x => x.CarrierName).HasMaxLength(200);
            e.Property(x => x.MyPortName).HasMaxLength(200);
            e.Property(x => x.MyPort2Name).HasMaxLength(200);
            e.Property(x => x.PortOfDeliveryName).HasMaxLength(200);
            e.Property(x => x.VasServiceName).HasMaxLength(200);
            e.Property(x => x.VasId);
            e.Property(x => x.ExecutionPlace).HasMaxLength(200);
            e.Property(x => x.VasUom).HasMaxLength(50);
            e.Property(x => x.VasCurrencyCode).HasMaxLength(10);
            e.Property(x => x.VasNotes).HasMaxLength(2000);
            e.Property(x => x.RmbVwt).HasMaxLength(100);
            e.Property(x => x.PackageType1).HasMaxLength(100);
            e.Property(x => x.PackageType2).HasMaxLength(100);
            e.Property(x => x.PackageType3).HasMaxLength(100);
            e.Property(x => x.PackageType4).HasMaxLength(100);
            e.Property(x => x.PackageType5).HasMaxLength(100);
            e.Property(x => x.DeliveryCountryName).HasMaxLength(200);
            e.Property(x => x.DeliveryStateName).HasMaxLength(200);
            e.Property(x => x.DeliveryCityName).HasMaxLength(200);
            e.Property(x => x.DeliveryZipCode).HasMaxLength(50);
            e.Property(x => x.DescriptionOfGoods).HasMaxLength(2000);
            e.Property(x => x.ShipperRef2).HasMaxLength(200);
            e.Property(x => x.ConsigneeRef2).HasMaxLength(200);
            e.Property(x => x.AgentName).HasMaxLength(200);
            e.Property(x => x.NotesToBePrinted).HasMaxLength(2000);
            e.Property(x => x.Notes).HasMaxLength(4000);
            e.HasOne<QuoteType>().WithMany().HasForeignKey(x => x.QuoteTypeId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.PickupAddress).WithMany().HasForeignKey(x => x.PickupAddressId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.DeliveryAddress).WithMany().HasForeignKey(x => x.DeliveryAddressId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Address>(e =>
        {
            e.ToTable("Addresses");
            e.HasKey(x => x.Id);
            e.Property(x => x.AddressTypeName).HasMaxLength(200);
            e.Property(x => x.Description).HasMaxLength(500);
            e.Property(x => x.Name).HasMaxLength(300);
            e.Property(x => x.Address1).HasMaxLength(500);
            e.Property(x => x.Address2).HasMaxLength(500);
            e.Property(x => x.CountryName).HasMaxLength(200);
            e.Property(x => x.Phone).HasMaxLength(100);
            e.Property(x => x.StateName).HasMaxLength(200);
            e.Property(x => x.Fax).HasMaxLength(100);
            e.Property(x => x.CityName).HasMaxLength(200);
            e.Property(x => x.Attn).HasMaxLength(200);
            e.Property(x => x.ZipCode).HasMaxLength(50);
            e.Property(x => x.Notes).HasMaxLength(2000);
            e.Property(x => x.FullAddressDisplay).HasMaxLength(1000);
        });
    }
}

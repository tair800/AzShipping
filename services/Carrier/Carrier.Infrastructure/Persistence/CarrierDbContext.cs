using Carrier.Domain.AggregatesModel.AirlineAggregate;
using Carrier.Domain.AggregatesModel.RailwayStationAggregate;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using Carrier.Domain.AggregatesModel.ShippingAgentAggregate;
using Carrier.Domain.AggregatesModel.DriverAggregate;
using Carrier.Domain.AggregatesModel.ShippingLineAggregate;
using Carrier.Domain.AggregatesModel.TerminalAggregate;
using Carrier.Domain.AggregatesModel.VehicleAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence;

public class CarrierDbContext : DbContext
{
    public CarrierDbContext(DbContextOptions<CarrierDbContext> options) : base(options) { }

    public DbSet<global::Carrier.Domain.AggregatesModel.CarrierAggregate.Carrier> Carriers { get; set; }
    public DbSet<Terminal> Terminals { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleBrand> VehicleBrands { get; set; }
    public DbSet<VehicleModel> VehicleModels { get; set; }
    public DbSet<EuroEmissionClass> EuroEmissionClasses { get; set; }
    public DbSet<VehicleGroup> VehicleGroups { get; set; }
    public DbSet<CarrierContactPerson> CarrierContactPersons { get; set; }
    public DbSet<CarrierBankAccount> CarrierBankAccounts { get; set; }
    public DbSet<CarrierManager> CarrierManagers { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<DriverCarrier> DriverCarriers { get; set; }
    public DbSet<DriverDrivingLicenceCategory> DriverDrivingLicenceCategories { get; set; }
    public DbSet<CarrierDirection> CarrierDirections { get; set; }
    public DbSet<CarrierDirectionTransportType> CarrierDirectionTransportTypes { get; set; }
    public DbSet<CarrierDocument> CarrierDocuments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }
    public DbSet<ShippingLine> ShippingLines { get; set; }
    public DbSet<Airline> Airlines { get; set; }
    public DbSet<ShippingAgent> ShippingAgents { get; set; }
    public DbSet<RailwayStation> RailwayStations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<global::Carrier.Domain.AggregatesModel.CarrierAggregate.Carrier>(e =>
        {
            e.ToTable("Carriers");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.LocalName).HasMaxLength(300);
            e.Property(x => x.ClientAdsCode).HasMaxLength(50);
            e.Property(x => x.Okpo).HasMaxLength(50);
            e.Property(x => x.Bin).HasMaxLength(50);
            e.Property(x => x.Ogrn).HasMaxLength(50);
            e.Property(x => x.Tin).HasMaxLength(50);
            e.Property(x => x.Rrc).HasMaxLength(50);
            e.Property(x => x.VatNumber).HasMaxLength(50);
            e.Property(x => x.CarrierDirection).HasMaxLength(100);
            e.Property(x => x.LegalZipCode).HasMaxLength(20);
            e.Property(x => x.LegalPhones).HasMaxLength(500);
            e.Property(x => x.LegalFax).HasMaxLength(500);
            e.Property(x => x.LegalEmails).HasMaxLength(500);
            e.Property(x => x.PostalZipCode).HasMaxLength(20);
            e.Property(x => x.PostalPhones).HasMaxLength(500);
            e.Property(x => x.PostalFax).HasMaxLength(500);
            e.Property(x => x.PostalEmails).HasMaxLength(500);
            e.Property(x => x.Comment).HasMaxLength(2000);
            e.HasMany(x => x.ContactPersons).WithOne(x => x.Carrier).HasForeignKey(x => x.CarrierId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.BankAccounts).WithOne(x => x.Carrier).HasForeignKey(x => x.CarrierId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.Managers).WithOne(x => x.Carrier).HasForeignKey(x => x.CarrierId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.Directions).WithOne(x => x.Carrier).HasForeignKey(x => x.CarrierId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.Documents).WithOne(x => x.Carrier).HasForeignKey(x => x.CarrierId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.Projects).WithOne(x => x.Carrier).HasForeignKey(x => x.CarrierId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Project>(e =>
        {
            e.ToTable("Projects");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<ProjectTask>(e =>
        {
            e.ToTable("Tasks");
            e.HasKey(x => x.Id);
            e.Property(x => x.TaskNo).IsRequired().HasMaxLength(50);
            e.Property(x => x.TaskName).HasMaxLength(500);
            e.HasOne(x => x.Project).WithMany(x => x.Tasks).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CarrierDocument>(e =>
        {
            e.ToTable("CarrierDocuments");
            e.HasKey(x => x.Id);
            e.Property(x => x.DocumentNumber).HasMaxLength(100);
            e.Property(x => x.DocumentName).HasMaxLength(200);
            e.Property(x => x.Comments).HasMaxLength(2000);
            e.Property(x => x.FilePath).HasMaxLength(500);
        });

        modelBuilder.Entity<CarrierDirection>(e =>
        {
            e.ToTable("CarrierDirections");
            e.HasKey(x => x.Id);
            e.Property(x => x.CarrierLicences).HasMaxLength(500);
            e.Property(x => x.Comments).HasMaxLength(2000);
            e.HasMany(x => x.TransportTypes).WithOne(x => x.CarrierDirection).HasForeignKey(x => x.CarrierDirectionId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CarrierDirectionTransportType>(e =>
        {
            e.ToTable("CarrierDirectionTransportTypes");
            e.HasKey(x => new { x.CarrierDirectionId, x.TransportTypeId });
        });

        modelBuilder.Entity<CarrierContactPerson>(e =>
        {
            e.ToTable("CarrierContactPersons");
            e.HasKey(x => x.Id);
            e.Property(x => x.EnglishName).HasMaxLength(200);
            e.Property(x => x.Position).HasMaxLength(100);
            e.Property(x => x.Emails).HasMaxLength(500);
            e.Property(x => x.Phones).HasMaxLength(500);
            e.Property(x => x.Fax).HasMaxLength(200);
        });

        modelBuilder.Entity<CarrierBankAccount>(e =>
        {
            e.ToTable("CarrierBankAccounts");
            e.HasKey(x => x.Id);
            e.Property(x => x.CurrencyCode).HasMaxLength(10);
            e.Property(x => x.AccountNumber).HasMaxLength(100);
            e.Property(x => x.TransitAccount).HasMaxLength(100);
            e.Property(x => x.CorrespondentBank).HasMaxLength(200);
            e.Property(x => x.CorrespondentAccount).HasMaxLength(100);
        });

        modelBuilder.Entity<CarrierManager>(e =>
        {
            e.ToTable("CarrierManagers");
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<Terminal>(e =>
        {
            e.ToTable("Terminals");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.Address).HasMaxLength(500);
            e.Property(x => x.PostCode).HasMaxLength(20);
            e.Property(x => x.RailwayStation).HasMaxLength(200);
            e.Property(x => x.TransportTypeIds).HasMaxLength(500);
            e.Property(x => x.Notes).HasMaxLength(2000);
        });

        modelBuilder.Entity<Vehicle>(e =>
        {
            e.ToTable("Vehicles");
            e.HasKey(x => x.Id);
            e.Property(x => x.VehicleNumber).IsRequired().HasMaxLength(100);
            e.HasOne<global::Carrier.Domain.AggregatesModel.CarrierAggregate.Carrier>()
                .WithMany().HasForeignKey(x => x.CarrierId).OnDelete(DeleteBehavior.SetNull);
            e.Property(x => x.BrandName).HasMaxLength(100);
            e.Property(x => x.ModelName).HasMaxLength(100);
            e.Property(x => x.TrailerNumber).HasMaxLength(100);
            e.Property(x => x.BodyNumber).HasMaxLength(100);
            e.Property(x => x.LicenceNumber).HasMaxLength(100);
            e.Property(x => x.Drivers).HasMaxLength(500);
            e.Property(x => x.FuelCard).HasMaxLength(100);
            e.Property(x => x.TransportInformation).HasMaxLength(500);
            e.Property(x => x.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<VehicleBrand>(e =>
        {
            e.ToTable("VehicleBrands");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<VehicleModel>(e =>
        {
            e.ToTable("VehicleModels");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<EuroEmissionClass>(e =>
        {
            e.ToTable("EuroEmissionClasses");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<VehicleGroup>(e =>
        {
            e.ToTable("VehicleGroups");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Driver>(e =>
        {
            e.ToTable("Drivers");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(200);
            e.Property(x => x.Surname).HasMaxLength(200);
            e.Property(x => x.MiddleName).HasMaxLength(200);
            e.Property(x => x.Passport).HasMaxLength(100);
            e.Property(x => x.DrivingLicenceNumber).HasMaxLength(100);
            e.Property(x => x.PhoneNumber).HasMaxLength(100);
            e.Property(x => x.BankAccount).HasMaxLength(200);
            e.Property(x => x.FuelCard).HasMaxLength(100);
            e.Property(x => x.Notes).HasMaxLength(2000);
            e.Property(x => x.PassportFilePath).HasMaxLength(500);
            e.Property(x => x.DrivingLicenceFilePath).HasMaxLength(500);
            e.HasMany(x => x.DriverCarriers).WithOne(x => x.Driver).HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.DrivingLicenceCategories).WithOne(x => x.Driver).HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DriverCarrier>(e =>
        {
            e.ToTable("DriverCarriers");
            e.HasKey(x => new { x.DriverId, x.CarrierId });
            e.HasOne(x => x.Carrier).WithMany().HasForeignKey(x => x.CarrierId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DriverDrivingLicenceCategory>(e =>
        {
            e.ToTable("DriverDrivingLicenceCategories");
            e.HasKey(x => new { x.DriverId, x.DrivingLicenceCategoryId });
        });

        modelBuilder.Entity<ShippingLine>(e =>
        {
            e.ToTable("ShippingLines");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).HasMaxLength(50);
            e.Property(x => x.ScacCode).HasMaxLength(50);
            e.Property(x => x.Cbsa).HasMaxLength(100);
            e.Property(x => x.Caat).HasMaxLength(100);
            e.Property(x => x.Name).HasMaxLength(200);
            e.Property(x => x.LocalName).HasMaxLength(300);
            e.Property(x => x.ShippingAgent).HasMaxLength(200);
            e.Property(x => x.Website).HasMaxLength(500);
            e.Property(x => x.VatNo).HasMaxLength(50);
            e.Property(x => x.Notes).HasMaxLength(2000);
        });

        modelBuilder.Entity<Airline>(e =>
        {
            e.ToTable("Airlines");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).HasMaxLength(50);
            e.Property(x => x.Icao).HasMaxLength(20);
            e.Property(x => x.Name).HasMaxLength(200);
            e.Property(x => x.LocalName).HasMaxLength(300);
            e.Property(x => x.Prefix).HasMaxLength(50);
            e.Property(x => x.Website).HasMaxLength(500);
            e.Property(x => x.VatNo).HasMaxLength(50);
            e.Property(x => x.Notes).HasMaxLength(2000);
        });

        modelBuilder.Entity<ShippingAgent>(e =>
        {
            e.ToTable("ShippingAgents");
            e.HasKey(x => x.Id);
            e.Property(x => x.CompanyName).HasMaxLength(200);
            e.Property(x => x.LocalName).HasMaxLength(300);
            e.Property(x => x.Address1).HasMaxLength(500);
            e.Property(x => x.Address2).HasMaxLength(500);
            e.Property(x => x.ZipCode).HasMaxLength(20);
            e.Property(x => x.VatNo).HasMaxLength(50);
            e.Property(x => x.Email).HasMaxLength(200);
            e.Property(x => x.EnglishName).HasMaxLength(200);
            e.Property(x => x.Position).HasMaxLength(100);
            e.Property(x => x.BusinessPhone).HasMaxLength(100);
            e.Property(x => x.Mobile).HasMaxLength(100);
            e.Property(x => x.Fax).HasMaxLength(100);
            e.Property(x => x.Phone).HasMaxLength(100);
            e.Property(x => x.Notes).HasMaxLength(2000);
        });

        modelBuilder.Entity<RailwayStation>(e =>
        {
            e.ToTable("RailwayStations");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).HasMaxLength(50);
            e.Property(x => x.Name).HasMaxLength(200);
            e.Property(x => x.Railway).HasMaxLength(200);
            e.Property(x => x.LocalName).HasMaxLength(300);
            e.Property(x => x.Prefix).HasMaxLength(50);
            e.Property(x => x.Website).HasMaxLength(500);
            e.Property(x => x.VatNo).HasMaxLength(50);
            e.Property(x => x.Notes).HasMaxLength(2000);
        });

    }
}

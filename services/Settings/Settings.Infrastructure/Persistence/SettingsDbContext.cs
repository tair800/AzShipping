using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.BankAggregate;
using Settings.Domain.AggregatesModel.CarrierTypeAggregate;
using Settings.Domain.AggregatesModel.ClientSegmentAggregate;
using Settings.Domain.AggregatesModel.DeferredPaymentConditionAggregate;
using Settings.Domain.AggregatesModel.DrivingLicenceCategoryAggregate;
using Settings.Domain.AggregatesModel.LoadingMethodAggregate;
using Settings.Domain.AggregatesModel.PackagingAggregate;
using Settings.Domain.AggregatesModel.RequestPurposeAggregate;
using Settings.Domain.AggregatesModel.RequestSourceAggregate;
using Settings.Domain.AggregatesModel.QuoteSourceAggregate;
using Settings.Domain.AggregatesModel.SalesFunnelStatusAggregate;
using Settings.Domain.AggregatesModel.TransportTypeAggregate;
using Settings.Domain.AggregatesModel.WorkerPostAggregate;
using Settings.Domain.AggregatesModel.WayOfNegotiationAggregate;
using Settings.Domain.AggregatesModel.ResultTypeAggregate;
using Settings.Domain.AggregatesModel.FunnelResultAggregate;
using Settings.Domain.AggregatesModel.StateAggregate;
using Settings.Domain.AggregatesModel.CityAggregate;
using Settings.Domain.AggregatesModel.GlobalZoneAggregate;
using Settings.Domain.AggregatesModel.CountryAggregate;
using Settings.Domain.AggregatesModel.ClientSourceAggregate;
using Settings.Domain.AggregatesModel.CompanyAggregate;
using Settings.Domain.AggregatesModel.ExecutionPlaceAggregate;
using Settings.Domain.AggregatesModel.MeetingTypeAggregate;
using TaskStatusEntity = Settings.Domain.AggregatesModel.TaskStatusAggregate.TaskStatus;
using Settings.Domain.AggregatesModel.TaskPriorityAggregate;
using Settings.Domain.AggregatesModel.MeetingStatusAggregate;
using Settings.Domain.AggregatesModel.MeetingResultAggregate;
using Settings.Domain.AggregatesModel.MeetingPriorityAggregate;
using Settings.Domain.AggregatesModel.UomAggregate;
using Settings.Domain.AggregatesModel.PricingTypeAggregate;
using Settings.Domain.AggregatesModel.DepartmentAggregate;
using Settings.Domain.AggregatesModel.AddressTypeAggregate;
using Settings.Domain.AggregatesModel.GeneralSettingAggregate;
using Settings.Domain.AggregatesModel.NumerationAggregate;
using Settings.Domain.AggregatesModel.SystemLogAggregate;
using Settings.Domain.AggregatesModel.ActionLogAggregate;
using Settings.Domain.AggregatesModel.MessageLogAggregate;
using Settings.Domain.AggregatesModel.TemplateAggregate;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

namespace Settings.Infrastructure.Persistence;

public class SettingsDbContext : DbContext
{
    public SettingsDbContext(DbContextOptions<SettingsDbContext> options) : base(options) { }

    public DbSet<Bank> Banks { get; set; }
    public DbSet<CarrierType> CarrierTypes { get; set; }
    public DbSet<ClientSegment> ClientSegments { get; set; }
    public DbSet<RequestSource> RequestSources { get; set; }
    public DbSet<QuoteSource> QuoteSources { get; set; }
    public DbSet<SalesFunnelStatus> SalesFunnelStatuses { get; set; }
    public DbSet<TransportType> TransportTypes { get; set; }
    public DbSet<RequestPurpose> RequestPurposes { get; set; }
    public DbSet<DeferredPaymentCondition> DeferredPaymentConditions { get; set; }
    public DbSet<Packaging> Packagings { get; set; }
    public DbSet<PackagingTranslation> PackagingTranslations { get; set; }
    public DbSet<LoadingMethod> LoadingMethods { get; set; }
    public DbSet<LoadingMethodTranslation> LoadingMethodTranslations { get; set; }
    public DbSet<WorkerPost> WorkerPosts { get; set; }
    public DbSet<WorkerPostTranslation> WorkerPostTranslations { get; set; }
    public DbSet<DrivingLicenceCategory> DrivingLicenceCategories { get; set; }
    public DbSet<WayOfNegotiation> WayOfNegotiations { get; set; }
    public DbSet<WayOfNegotiationTranslation> WayOfNegotiationTranslations { get; set; }
    public DbSet<ResultType> ResultTypes { get; set; }
    public DbSet<FunnelResult> FunnelResults { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<GlobalZone> GlobalZones { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<CountryGlobalZone> CountryGlobalZones { get; set; }
    public DbSet<ClientSource> ClientSources { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyBankAccount> CompanyBankAccounts { get; set; }
    public DbSet<CompanySignature> CompanySignatures { get; set; }
    public DbSet<ExecutionPlace> ExecutionPlaces { get; set; }
    public DbSet<MeetingType> MeetingTypes { get; set; }
    public DbSet<TaskStatusEntity> TaskStatuses { get; set; }
    public DbSet<TaskPriority> TaskPriorities { get; set; }
    public DbSet<MeetingStatus> MeetingStatuses { get; set; }
    public DbSet<MeetingResult> MeetingResults { get; set; }
    public DbSet<MeetingPriority> MeetingPriorities { get; set; }
    public DbSet<Uom> Uoms { get; set; }
    public DbSet<PricingType> PricingTypes { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<GeneralSetting> GeneralSettings { get; set; }
    public DbSet<Numeration> Numerations { get; set; }
    public DbSet<SystemLog> SystemLogs { get; set; }
    public DbSet<ActionLog> ActionLogs { get; set; }
    public DbSet<MessageLog> MessageLogs { get; set; }
    public DbSet<Template> Templates { get; set; }
    public DbSet<TemplateTranslation> TemplateTranslations { get; set; }
    public DbSet<EmailAccountSetting> EmailAccountSettings { get; set; }
    public DbSet<EmployeeGroup> EmployeeGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Bank>(e =>
        {
            e.ToTable("Banks");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<CarrierType>(e => { e.ToTable("CarrierTypes"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); });
        modelBuilder.Entity<ExecutionPlace>(e => { e.ToTable("ExecutionPlaces"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); });
        modelBuilder.Entity<MeetingType>(e => { e.ToTable("MeetingTypes"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); });
        modelBuilder.Entity<TaskStatusEntity>(e => { e.ToTable("TaskStatuses"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); e.Property(x => x.PrimaryColor).HasMaxLength(20); e.Property(x => x.SecondaryColor).HasMaxLength(20); });
        modelBuilder.Entity<TaskPriority>(e => { e.ToTable("TaskPriorities"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); e.Property(x => x.PrimaryColor).HasMaxLength(20); e.Property(x => x.SecondaryColor).HasMaxLength(20); });
        modelBuilder.Entity<MeetingStatus>(e => { e.ToTable("MeetingStatuses"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); e.Property(x => x.PrimaryColor).HasMaxLength(20); e.Property(x => x.SecondaryColor).HasMaxLength(20); });
        modelBuilder.Entity<MeetingResult>(e => { e.ToTable("MeetingResults"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); e.Property(x => x.PrimaryColor).HasMaxLength(20); e.Property(x => x.SecondaryColor).HasMaxLength(20); });
        modelBuilder.Entity<MeetingPriority>(e => { e.ToTable("MeetingPriorities"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); e.Property(x => x.PrimaryColor).HasMaxLength(20); e.Property(x => x.SecondaryColor).HasMaxLength(20); });
        modelBuilder.Entity<Uom>(e => { e.ToTable("Uoms"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(50); });
        modelBuilder.Entity<PricingType>(e => { e.ToTable("PricingTypes"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); });
        modelBuilder.Entity<Department>(e =>
        {
            e.ToTable("Departments");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.Prefix).HasMaxLength(50);
            e.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<AddressType>(e =>
        {
            e.ToTable("AddressTypes");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).IsRequired().HasMaxLength(50);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.Description).HasMaxLength(500);
        });
        modelBuilder.Entity<GeneralSetting>(e =>
        {
            e.ToTable("GeneralSettings");
            e.HasKey(x => x.Id);
            e.Property(x => x.LogoPath).HasMaxLength(500);
            e.Property(x => x.CurrencyCode).HasMaxLength(20);
            e.Property(x => x.DateFormat).HasMaxLength(50);
            e.Property(x => x.PriceDisplayType).HasMaxLength(50);
            e.Property(x => x.DefaultLanguageCode).HasMaxLength(20);
            e.Property(x => x.NotificationLanguageCode).HasMaxLength(20);
            e.Property(x => x.BankCode).HasMaxLength(50);
            e.Property(x => x.Timezone).HasMaxLength(100);
        });
        modelBuilder.Entity<Numeration>(e =>
        {
            e.ToTable("Numerations");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.NumerationForCode).IsRequired().HasMaxLength(100);
            e.Property(x => x.ElementCode).HasMaxLength(100);
            e.Property(x => x.DocumentTypeCode).HasMaxLength(100);
            e.Property(x => x.Formula).IsRequired().HasMaxLength(500);
            e.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.Department).WithMany().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.SetNull);
            e.HasIndex(x => new
            {
                x.NumerationForCode, x.CompanyId, x.DepartmentId, x.ClientId, x.EmployeeId, x.ElementCode, x.DocumentTypeCode
            });
        });
        modelBuilder.Entity<SystemLog>(e =>
        {
            e.ToTable("SystemLogs");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).UseIdentityByDefaultColumn();
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.Level).IsRequired().HasMaxLength(50);
            e.Property(x => x.Body).IsRequired();
            e.HasIndex(x => x.CreatedAt);
        });
        modelBuilder.Entity<ActionLog>(e =>
        {
            e.ToTable("ActionLogs");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).UseIdentityByDefaultColumn();
            e.Property(x => x.Action).IsRequired().HasMaxLength(200);
            e.Property(x => x.Data).IsRequired();
            e.Property(x => x.SessionId).HasMaxLength(100);
            e.Property(x => x.IpAddress).HasMaxLength(50);
            e.Property(x => x.Location).HasMaxLength(200);
            e.Property(x => x.Browser).HasMaxLength(200);
            e.Property(x => x.EmployeeName).HasMaxLength(200);
            e.HasIndex(x => x.CreatedAt);
        });
        modelBuilder.Entity<MessageLog>(e =>
        {
            e.ToTable("MessageLogs");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).UseIdentityByDefaultColumn();
            e.Property(x => x.Sender).IsRequired().HasMaxLength(200);
            e.Property(x => x.Receiver).IsRequired().HasMaxLength(200);
            e.Property(x => x.CompanyName).HasMaxLength(300);
            e.Property(x => x.Theme).IsRequired().HasMaxLength(500);
            e.Property(x => x.Body).IsRequired();
            e.Property(x => x.LinkUrl).HasMaxLength(500);
            e.Property(x => x.LinkText).HasMaxLength(200);
            e.HasIndex(x => x.SentAt);
        });
        modelBuilder.Entity<ClientSegment>(e => { e.ToTable("ClientSegments"); e.HasKey(x => x.Id); e.Property(x => x.SegmentName).IsRequired().HasMaxLength(200); });
        modelBuilder.Entity<RequestSource>(e => { e.ToTable("RequestSources"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); });
        modelBuilder.Entity<QuoteSource>(e =>
        {
            e.ToTable("QuoteSources");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
        });
        modelBuilder.Entity<SalesFunnelStatus>(e => { e.ToTable("SalesFunnelStatuses"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); });
        modelBuilder.Entity<TransportType>(e => { e.ToTable("TransportTypes"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); });
        modelBuilder.Entity<RequestPurpose>(e => { e.ToTable("RequestPurposes"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); });
        modelBuilder.Entity<DeferredPaymentCondition>(e => { e.ToTable("DeferredPaymentConditions"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); });
        modelBuilder.Entity<DrivingLicenceCategory>(e => { e.ToTable("DrivingLicenceCategories"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(200); });

        modelBuilder.Entity<Packaging>(e =>
        {
            e.ToTable("Packagings");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasMany(x => x.Translations).WithOne(x => x.Packaging).HasForeignKey(x => x.PackagingId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<PackagingTranslation>(e => { e.ToTable("PackagingTranslations"); e.HasKey(x => x.Id); e.Property(x => x.LanguageCode).HasMaxLength(10); e.Property(x => x.Name).HasMaxLength(200); });

        modelBuilder.Entity<LoadingMethod>(e =>
        {
            e.ToTable("LoadingMethods");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasMany(x => x.Translations).WithOne(x => x.LoadingMethod).HasForeignKey(x => x.LoadingMethodId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<LoadingMethodTranslation>(e => { e.ToTable("LoadingMethodTranslations"); e.HasKey(x => x.Id); e.Property(x => x.LanguageCode).HasMaxLength(10); e.Property(x => x.Name).HasMaxLength(200); });

        modelBuilder.Entity<WorkerPost>(e =>
        {
            e.ToTable("WorkerPosts");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasMany(x => x.Translations).WithOne(x => x.WorkerPost).HasForeignKey(x => x.WorkerPostId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<WorkerPostTranslation>(e => { e.ToTable("WorkerPostTranslations"); e.HasKey(x => x.Id); e.Property(x => x.LanguageCode).HasMaxLength(10); e.Property(x => x.Name).HasMaxLength(200); });

        modelBuilder.Entity<Template>(e =>
        {
            e.ToTable("Templates");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasMany(x => x.Translations).WithOne(x => x.Template).HasForeignKey(x => x.TemplateId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<TemplateTranslation>(e => { e.ToTable("TemplateTranslations"); e.HasKey(x => x.Id); e.Property(x => x.LanguageCode).HasMaxLength(10); e.Property(x => x.Name).HasMaxLength(200); });

        modelBuilder.Entity<WayOfNegotiation>(e =>
        {
            e.ToTable("WayOfNegotiations");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasMany(x => x.Translations).WithOne(x => x.WayOfNegotiation).HasForeignKey(x => x.WayOfNegotiationId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<WayOfNegotiationTranslation>(e => { e.ToTable("WayOfNegotiationTranslations"); e.HasKey(x => x.Id); e.Property(x => x.LanguageCode).HasMaxLength(10); e.Property(x => x.Name).HasMaxLength(200); });

        modelBuilder.Entity<ResultType>(e => { e.ToTable("ResultTypes"); e.HasKey(x => x.Id); e.Property(x => x.Name).IsRequired().HasMaxLength(100); e.Property(x => x.Code).IsRequired().HasMaxLength(20); });
        modelBuilder.Entity<FunnelResult>(e =>
        {
            e.ToTable("FunnelResults");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasOne(x => x.ResultType).WithMany().HasForeignKey(x => x.ResultTypeId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<State>(e =>
        {
            e.ToTable("States");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).IsRequired().HasMaxLength(20);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.LocalName).HasMaxLength(200);
            e.Property(x => x.Notes).HasMaxLength(500);
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
            e.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<City>(e =>
        {
            e.ToTable("Cities");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).IsRequired().HasMaxLength(20);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.LocalName).HasMaxLength(200);
            e.Property(x => x.ZipCode).HasMaxLength(20);
            e.Property(x => x.Notes).HasMaxLength(500);
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
            e.HasOne(x => x.State).WithMany().HasForeignKey(x => x.StateId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<GlobalZone>(e =>
        {
            e.ToTable("GlobalZones");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).IsRequired().HasMaxLength(20);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.LocalName).HasMaxLength(200);
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        });

        modelBuilder.Entity<Country>(e =>
        {
            e.ToTable("Countries");
            e.HasKey(x => x.Id);
            e.Property(x => x.IsoCode).IsRequired().HasMaxLength(10);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.LocalName).HasMaxLength(200);
            e.Property(x => x.Notes).HasMaxLength(500);
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        });

        modelBuilder.Entity<CountryGlobalZone>(e =>
        {
            e.ToTable("CountryGlobalZones");
            e.HasKey(x => new { x.CountryId, x.GlobalZoneId });
            e.HasOne(x => x.Country).WithMany(c => c.CountryGlobalZones).HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.GlobalZone).WithMany().HasForeignKey(x => x.GlobalZoneId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ClientSource>(e =>
        {
            e.ToTable("ClientSources");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Company>(e =>
        {
            e.ToTable("Companies");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.NameFull).HasMaxLength(500);
            e.Property(x => x.DirectorsFullName).HasMaxLength(200);
            e.Property(x => x.InTheNameOfWhom).HasMaxLength(200);
            e.Property(x => x.Post).HasMaxLength(100);
            e.Property(x => x.VatRate).HasMaxLength(100);
            e.Property(x => x.PricingType).HasMaxLength(100);
            e.Property(x => x.CompanyPrefix).HasMaxLength(20);
            e.Property(x => x.CompanyCodeType).HasMaxLength(50);
            e.Property(x => x.CompanyCode).HasMaxLength(100);
            e.Property(x => x.VatCode).HasMaxLength(100);
            e.Property(x => x.Rrc).HasMaxLength(100);
            e.Property(x => x.CorrespondentAccount).HasMaxLength(100);
            e.Property(x => x.Okpo).HasMaxLength(50);
            e.Property(x => x.Ogrn).HasMaxLength(50);
            e.Property(x => x.Address).HasMaxLength(500);
            e.Property(x => x.PostCode).HasMaxLength(50);
            e.Property(x => x.Telephone).HasMaxLength(50);
            e.Property(x => x.Fax).HasMaxLength(50);
            e.Property(x => x.Email).HasMaxLength(200);
            e.Property(x => x.Website).HasMaxLength(200);
            e.Property(x => x.CorrespondentAddress).HasMaxLength(500);
            e.Property(x => x.CorrespondentPostCode).HasMaxLength(50);
            e.Property(x => x.CorrespondentTelephone).HasMaxLength(50);
            e.Property(x => x.CorrespondentFax).HasMaxLength(50);
            e.Property(x => x.CorrespondentEmail).HasMaxLength(200);
            e.Property(x => x.CorrespondentWebsite).HasMaxLength(200);
            e.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.State).WithMany().HasForeignKey(x => x.StateId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.WorkerPost).WithMany().HasForeignKey(x => x.WorkerPostId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.PricingTypeEntity).WithMany().HasForeignKey(x => x.PricingTypeId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.CorrespondentCountry).WithMany().HasForeignKey(x => x.CorrespondentCountryId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.CorrespondentState).WithMany().HasForeignKey(x => x.CorrespondentStateId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.CorrespondentCity).WithMany().HasForeignKey(x => x.CorrespondentCityId).OnDelete(DeleteBehavior.SetNull);
            e.HasMany(x => x.BankAccounts).WithOne(x => x.Company).HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.Signatures).WithOne(x => x.Company).HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CompanyBankAccount>(e =>
        {
            e.ToTable("CompanyBankAccounts");
            e.HasKey(x => x.Id);
            e.Property(x => x.CurrencyCode).HasMaxLength(10);
            e.Property(x => x.AccountNumberIban).HasMaxLength(100);
            e.Property(x => x.BankCode).HasMaxLength(50);
            e.Property(x => x.Swift).HasMaxLength(50);
            e.Property(x => x.TransitAmount).HasMaxLength(100);
            e.Property(x => x.CorrespondentAccount).HasMaxLength(100);
        });

        modelBuilder.Entity<CompanySignature>(e =>
        {
            e.ToTable("CompanySignatures");
            e.HasKey(x => x.Id);
            e.Property(x => x.Type).HasMaxLength(50);
            e.Property(x => x.FileName).HasMaxLength(255);
            e.Property(x => x.FilePath).HasMaxLength(500);
            e.Property(x => x.SignatoryName).HasMaxLength(200);
            e.Property(x => x.Role).HasMaxLength(100);
        });

        modelBuilder.Entity<EmailAccountSetting>(e =>
        {
            e.ToTable("EmailAccountSettings");
            e.HasKey(x => x.Id);
            e.Property(x => x.AccountEmail).IsRequired().HasMaxLength(320);
            e.HasIndex(x => x.AccountEmail).IsUnique();
            e.Property(x => x.SmtpAuthUsername).HasMaxLength(320);
            e.Property(x => x.ConnectionMode).IsRequired().HasMaxLength(64);
            e.Property(x => x.SmtpHost).IsRequired().HasMaxLength(255);
            e.Property(x => x.SmtpSecurity).IsRequired().HasMaxLength(32);
            e.Property(x => x.LinkedUserDisplayName).HasMaxLength(300);
        });

        modelBuilder.Entity<EmployeeGroup>(e =>
        {
            e.ToTable("EmployeeGroups");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.PermissionsJson).IsRequired().HasColumnType("jsonb");
            e.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.SetNull);
        });
    }
}

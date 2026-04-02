using AzShipping.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ClientSegment> ClientSegments { get; set; }
    public DbSet<RequestSource> RequestSources { get; set; }
    public DbSet<Packaging> Packagings { get; set; }
    public DbSet<PackagingTranslation> PackagingTranslations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SalesFunnelStatus> SalesFunnelStatuses { get; set; }
    public DbSet<SalesFunnelStatusTranslation> SalesFunnelStatusTranslations { get; set; }
    public DbSet<TransportType> TransportTypes { get; set; }
    public DbSet<TransportTypeTranslation> TransportTypeTranslations { get; set; }
    public DbSet<LoadingMethod> LoadingMethods { get; set; }
    public DbSet<LoadingMethodTranslation> LoadingMethodTranslations { get; set; }
    public DbSet<DeferredPaymentCondition> DeferredPaymentConditions { get; set; }
    public DbSet<RequestPurpose> RequestPurposes { get; set; }
    public DbSet<DrivingLicenceCategory> DrivingLicenceCategories { get; set; }
    public DbSet<WorkerPost> WorkerPosts { get; set; }
    public DbSet<WorkerPostTranslation> WorkerPostTranslations { get; set; }
    public DbSet<CarrierType> CarrierTypes { get; set; }
    public DbSet<Bank> Banks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ClientSegment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SegmentName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.SegmentPriority).IsRequired();
            entity.Property(e => e.PrimaryColor).IsRequired().HasMaxLength(7);
            entity.Property(e => e.SecondaryColor).IsRequired().HasMaxLength(7);
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<RequestSource>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<Packaging>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasMany(e => e.Translations)
                  .WithOne(t => t.Packaging)
                  .HasForeignKey(t => t.PackagingId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PackagingTranslation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LanguageCode).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => new { e.PackagingId, e.LanguageCode }).IsUnique();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<SalesFunnelStatus>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.StatusPosition).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasOne(e => e.ResponsibleManager)
                  .WithMany()
                  .HasForeignKey(e => e.ResponsibleManagerId)
                  .OnDelete(DeleteBehavior.SetNull);
            entity.HasMany(e => e.Translations)
                  .WithOne(t => t.SalesFunnelStatus)
                  .HasForeignKey(t => t.SalesFunnelStatusId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SalesFunnelStatusTranslation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LanguageCode).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => new { e.SalesFunnelStatusId, e.LanguageCode }).IsUnique();
        });

        modelBuilder.Entity<TransportType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasMany(e => e.Translations)
                  .WithOne(t => t.TransportType)
                  .HasForeignKey(t => t.TransportTypeId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TransportTypeTranslation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LanguageCode).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => new { e.TransportTypeId, e.LanguageCode }).IsUnique();
        });

        modelBuilder.Entity<LoadingMethod>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasMany(e => e.Translations)
                  .WithOne(t => t.LoadingMethod)
                  .HasForeignKey(t => t.LoadingMethodId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LoadingMethodTranslation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LanguageCode).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => new { e.LoadingMethodId, e.LanguageCode }).IsUnique();
        });

        modelBuilder.Entity<DeferredPaymentCondition>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.FullText).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<RequestPurpose>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<DrivingLicenceCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(10);
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<WorkerPost>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasMany(e => e.Translations)
                  .WithOne(t => t.WorkerPost)
                  .HasForeignKey(t => t.WorkerPostId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<WorkerPostTranslation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LanguageCode).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => new { e.WorkerPostId, e.LanguageCode }).IsUnique();
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.UnofficialName).HasMaxLength(200);
            entity.Property(e => e.Branch).HasMaxLength(200);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Swift).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.PostCode).HasMaxLength(20);
            entity.Property(e => e.CreatedAt).IsRequired();
        });
    }
}


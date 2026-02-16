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
    }
}


using Clients.Domain.AggregatesModel.ClientAggregate;
using Clients.Domain.AggregatesModel.CurrencyAggregate;
using Clients.Domain.AggregatesModel.DirectionAggregate;
using Clients.Domain.AggregatesModel.DocumentAggregate;
using Clients.Domain.AggregatesModel.NegotiationAggregate;
using Microsoft.EntityFrameworkCore;

namespace Clients.Infrastructure.Persistence;

public class ClientsDbContext : DbContext
{
    public ClientsDbContext(DbContextOptions<ClientsDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientContactPerson> ClientContactPersons { get; set; }
    public DbSet<ClientBankAccount> ClientBankAccounts { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Direction> Directions { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Negotiation> Negotiations { get; set; }
    public DbSet<NegotiationResult> NegotiationResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(e =>
        {
            e.ToTable("Clients");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).IsRequired().HasMaxLength(50);
            e.Property(x => x.CompanyName).IsRequired().HasMaxLength(500);
            e.Property(x => x.NameAbbreviated).IsRequired().HasMaxLength(200);
            e.Property(x => x.VatNumber).HasMaxLength(50);
            e.Property(x => x.Inn).HasMaxLength(50);
            e.Property(x => x.Title).HasMaxLength(500);
            e.Property(x => x.Okpo).HasMaxLength(50);
            e.Property(x => x.Kpp).HasMaxLength(50);
            e.Property(x => x.Bin).HasMaxLength(50);
            e.Property(x => x.ClientAisCode).HasMaxLength(50);
            e.Property(x => x.ActivityAreaName).HasMaxLength(200);
            e.Property(x => x.AddressLine1).HasMaxLength(500);
            e.Property(x => x.Tin).HasMaxLength(50);
            e.Property(x => x.Ogrn).HasMaxLength(50);
            e.Property(x => x.PrimaryPhone).HasMaxLength(50);
            e.Property(x => x.GeneralFax).HasMaxLength(50);
            e.Property(x => x.LegalStreet).HasMaxLength(500);
            e.Property(x => x.LegalEmail).HasMaxLength(200);
            e.Property(x => x.PostalStreet).HasMaxLength(500);
            e.Property(x => x.PostalCityName).HasMaxLength(200);
            e.Property(x => x.PostalEmail).HasMaxLength(200);
            e.Property(x => x.EmailToSendDocuments).HasMaxLength(200);
            e.Property(x => x.Comment).HasMaxLength(2000);
            e.HasMany(x => x.ContactPersons).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.BankAccounts).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ClientContactPerson>(e =>
        {
            e.ToTable("ClientContactPersons");
            e.HasKey(x => x.Id);
            e.Property(x => x.EnglishName).HasMaxLength(200);
            e.Property(x => x.Phone).HasMaxLength(50);
            e.Property(x => x.Email).HasMaxLength(200);
            e.Property(x => x.Mobile).HasMaxLength(50);
            e.Property(x => x.Fax).HasMaxLength(50);
        });

        modelBuilder.Entity<ClientBankAccount>(e =>
        {
            e.ToTable("ClientBankAccounts");
            e.HasKey(x => x.Id);
            e.Property(x => x.AccountNumberIban).HasMaxLength(100);
            e.Property(x => x.TransitAmount).HasMaxLength(100);
            e.Property(x => x.CorrespondentAccount).HasMaxLength(100);
        });

        modelBuilder.Entity<Currency>(e =>
        {
            e.ToTable("Currencies");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).IsRequired().HasMaxLength(10);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Direction>(e =>
        {
            e.ToTable("Directions");
            e.HasKey(x => x.Id);
            e.Property(x => x.Note).HasMaxLength(500);
            e.Property(x => x.Comments).HasMaxLength(2000);
        });

        modelBuilder.Entity<Document>(e =>
        {
            e.ToTable("Documents");
            e.HasKey(x => x.Id);
            e.Property(x => x.DocumentType).HasMaxLength(20).HasDefaultValue("upload");
            e.Property(x => x.DocumentNumber).IsRequired().HasMaxLength(100);
            e.Property(x => x.DocumentName).IsRequired().HasMaxLength(200);
            e.Property(x => x.Comments).HasMaxLength(2000);
            e.Property(x => x.FilePath).HasMaxLength(500);
        });

        modelBuilder.Entity<Negotiation>(e =>
        {
            e.ToTable("Negotiations");
            e.HasKey(x => x.Id);
            e.Property(x => x.PersonName).IsRequired().HasMaxLength(200);
            e.Property(x => x.QuestionsAndAnswers).HasMaxLength(4000);
            e.Property(x => x.Result).HasMaxLength(2000);
        });

        modelBuilder.Entity<NegotiationResult>(e =>
        {
            e.ToTable("NegotiationResults");
            e.HasKey(x => x.Id);
            e.Property(x => x.Result).IsRequired().HasMaxLength(500);
            e.Property(x => x.Comments).HasMaxLength(2000);
            e.HasOne<Negotiation>().WithMany().HasForeignKey(x => x.NegotiationId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}

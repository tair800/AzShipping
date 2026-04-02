using General.Domain.AggregatesModel.CurrencyAggregate;
using General.Domain.AggregatesModel.IncotermAggregate;
using General.Domain.AggregatesModel.MeetingAggregate;
using General.Domain.AggregatesModel.MeetingHistoryAggregate;
using General.Domain.AggregatesModel.ProjectAggregate;
using General.Domain.AggregatesModel.VasAggregate;
using General.Domain.AggregatesModel.VesselAggregate;
using General.Domain.AggregatesModel.TaskAggregate;
using General.Domain.AggregatesModel.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence;

public class GeneralDbContext : DbContext
{
    public GeneralDbContext(DbContextOptions<GeneralDbContext> options) : base(options) { }

    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<GeneralTask> Tasks { get; set; }
    public DbSet<TaskDocument> TaskDocuments { get; set; }
    public DbSet<Incoterm> Incoterms { get; set; }
    public DbSet<Vas> Vas { get; set; }
    public DbSet<Vessel> Vessels { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingHistory> MeetingHistories { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeNote> EmployeeNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Currency>(e =>
        {
            e.ToTable("Currencies");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).IsRequired().HasMaxLength(3);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Symbol).HasMaxLength(10);
        });

        modelBuilder.Entity<Project>(e =>
        {
            e.ToTable("Projects");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<GeneralTask>(e =>
        {
            e.ToTable("Tasks");
            e.HasKey(x => x.Id);
            e.Property(x => x.TaskNo).IsRequired().HasMaxLength(50);
            e.Property(x => x.TaskName).IsRequired().HasMaxLength(500);
            e.Property(x => x.Comments).HasMaxLength(2000);
            e.HasOne(x => x.Project).WithMany(x => x.Tasks).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.SetNull);
            e.HasMany(x => x.Documents).WithOne(x => x.Task).HasForeignKey(x => x.TaskId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TaskDocument>(e =>
        {
            e.ToTable("TaskDocuments");
            e.HasKey(x => x.Id);
            e.Property(x => x.FilePath).IsRequired().HasMaxLength(500);
            e.Property(x => x.DocumentName).HasMaxLength(200);
        });

        modelBuilder.Entity<Incoterm>(e =>
        {
            e.ToTable("Incoterms");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).HasMaxLength(50);
            e.Property(x => x.Name).HasMaxLength(200);
            e.Property(x => x.LocalName).HasMaxLength(300);
            e.Property(x => x.Freight).HasMaxLength(50);
            e.Property(x => x.OtherCharges).HasMaxLength(50);
        });

        modelBuilder.Entity<Vas>(e =>
        {
            e.ToTable("Vas");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).HasMaxLength(50);
            e.Property(x => x.Name).HasMaxLength(200);
            e.Property(x => x.ExecutionPlace).HasMaxLength(200);
            e.Property(x => x.Uom).HasMaxLength(100);
            e.Property(x => x.Notes).HasMaxLength(2000);
            e.HasOne(x => x.Currency).WithMany().HasForeignKey(x => x.CurrencyId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Vessel>(e =>
        {
            e.ToTable("Vessels");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).HasMaxLength(50);
            e.Property(x => x.Name).HasMaxLength(200);
            e.Property(x => x.ImoCode).HasMaxLength(50);
            e.Property(x => x.LocalName).HasMaxLength(300);
            e.Property(x => x.Notes).HasMaxLength(2000);
        });

        modelBuilder.Entity<Meeting>(e =>
        {
            e.ToTable("Meetings");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.Time).HasMaxLength(50);
            e.Property(x => x.Address).HasMaxLength(500);
            e.Property(x => x.Comments).HasMaxLength(2000);
        });

        modelBuilder.Entity<MeetingHistory>(e =>
        {
            e.ToTable("MeetingHistories");
            e.HasKey(x => x.Id);
            e.Property(x => x.EventType).HasMaxLength(100);
            e.Property(x => x.Time).HasMaxLength(50);
            e.Property(x => x.FieldName).HasMaxLength(100);
            e.Property(x => x.OldValue).HasMaxLength(500);
            e.Property(x => x.NewValue).HasMaxLength(500);
            e.HasOne<Meeting>().WithMany().HasForeignKey(x => x.MeetingId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Employee>(e =>
        {
            e.ToTable("Employees");
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.UserId).IsUnique();
            e.Property(x => x.FullName).HasMaxLength(200);
            e.Property(x => x.Username).HasMaxLength(100);
            e.Property(x => x.Email).HasMaxLength(320);
            e.Property(x => x.Phone).HasMaxLength(50);
            e.Property(x => x.ContractNumber).HasMaxLength(100);
            e.Property(x => x.Address).HasMaxLength(500);
            e.Property(x => x.ProfileImageUrl).HasMaxLength(500);
        });

        modelBuilder.Entity<EmployeeNote>(e =>
        {
            e.ToTable("EmployeeNotes");
            e.HasKey(x => x.Id);
            e.Property(x => x.Content).IsRequired().HasMaxLength(4000);
            e.Property(x => x.NoteDate).HasColumnType("date");
            e.HasIndex(x => x.EmployeeId);
            e.HasOne<Employee>().WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}

using Identity.Domain.AggregatesModel.PermissionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        // Table Name
        builder.ToTable("Permissions");

        // Id, AutoIncrement
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
               .ValueGeneratedOnAdd()
               .IsRequired();

        // Permission Name
        builder.Property(p => p.Name)
               .HasMaxLength(100)
               .IsRequired()
               .HasComment("Permission Name");

        // Module Name
        builder.Property(p => p.Module)
               .HasMaxLength(100)
               .IsRequired()
               .HasComment("What Module does the Permission belong to");

        // Unique index, Search by permission Name and Module Name
        builder.HasIndex(p => new { p.Module, p.Name })
               .IsUnique()
               .HasDatabaseName("UQ_Permissions_Module_Name");

        // Optimistic concurrency
        builder.Property<uint>("xmin").IsRowVersion();
    }
}

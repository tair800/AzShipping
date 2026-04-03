using Identity.Domain.AggregatesModel.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Table Name
        builder.ToTable("Roles");

        // Id, AutoIncrement
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

        // Name, UniqueIndex
        builder.Property(r => r.Name)
               .HasMaxLength(50)
               .IsRequired()
               .HasComment("Role Name");
        builder.HasIndex(r => r.Name)
               .IsUnique()
               .HasDatabaseName("UQ_Roles_Name");

        builder.Navigation(r => r.RolePermissions)
               .UsePropertyAccessMode(PropertyAccessMode.Field);

        // Optimistic concurrency
        builder.Property<uint>("xmin").IsRowVersion();
    }
}
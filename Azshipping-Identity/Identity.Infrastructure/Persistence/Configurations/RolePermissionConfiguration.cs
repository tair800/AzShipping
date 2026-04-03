using Identity.Domain.AggregatesModel.PermissionAggregate;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Domain.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        // Table Name
        builder.ToTable("Roles_Permissions");

        // Composite Key
        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder.Property(rp => rp.RoleId)
               .IsRequired();

        builder.Property(rp => rp.PermissionId)
               .IsRequired();

        // Role Relationship
        // Each RolePermission links to a Role; one Role can have many Permissions
        builder.HasOne(rp => rp.Role)
               .WithMany(rp => rp.RolePermissions)
               .HasForeignKey(rp => rp.RoleId)
               .OnDelete(DeleteBehavior.Cascade);

        // Permission Relationship
        // Each RolePermission links to a Permission; one Permission can be assigned to many Roles
        builder.HasOne<Permission>()
               .WithMany()
               .HasForeignKey(rp => rp.PermissionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
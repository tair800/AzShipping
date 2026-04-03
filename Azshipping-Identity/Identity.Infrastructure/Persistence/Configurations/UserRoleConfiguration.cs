using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Domain.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        // Table Name
        builder.ToTable("Users_Roles");

        // Composite Key
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Property(ur => ur.UserId)
               .IsRequired();

        builder.Property(ur => ur.RoleId)
               .IsRequired();

        // User Relationship
        // Each UserRole links to a User; one User can have many Roles
        builder.HasOne(ur => ur.User)
               .WithMany(ur => ur.UserRoles)
               .HasForeignKey(ur => ur.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // Role Relationship
        // Each UserRole links to a Role; one Role can have many Users
        builder.HasOne<Role>()
               .WithMany()
               .HasForeignKey(ur => ur.RoleId)
               .OnDelete(DeleteBehavior.Restrict);

    }
}
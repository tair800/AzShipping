using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Domain.AggregatesModel.UserAggregate.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table Name
        builder.ToTable("Users");

        // Id, AutoIncrement
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
               .ValueGeneratedOnAdd()
               .IsRequired();

        // Username, (VO)
        builder.OwnsOne(u => u.Username, b =>
        {
            b.Property(p => p.Value)
             .HasColumnName("Username")
             .IsRequired()
             .HasMaxLength(50)
             .HasComment("Unique User Name");

           b.HasIndex(p => p.Value)
            .IsUnique()
            .HasDatabaseName("UQ_Users_Username");
        });


        // Password Hash, (vo)
        builder.OwnsOne(u => u.PasswordHash, b =>
        {
            b.Property(p => p.Value)
             .HasColumnName("PasswordHash")
             .IsRequired()
             .HasMaxLength(300)
             .HasComment("Username PasswordHash");
        });

        //FullName (VO)
        builder.OwnsOne(u => u.FullName, b =>
        {
            b.Property(p => p.Name)
             .HasColumnName("Name")
             .HasMaxLength(50)
             .HasComment("User's first name");

            b.Property(p => p.Surname)
             .HasColumnName("Surname")
             .HasMaxLength(50)
             .HasComment("User's last name");

        });

        // Email, (VO)
        builder.OwnsOne(u => u.Email, b =>
        {
            b.Property(p => p.Value)
             .HasColumnName("Email")
             .IsRequired()
             .HasMaxLength(100)
             .HasComment("Unique Email");

            b.HasIndex(p => p.Value)
             .IsUnique()
             .HasDatabaseName("UQ_Users_Email");
        });


        // Phone (VO)
        builder.OwnsOne(u => u.PhoneNumber, b =>
        {
            b.Property(p => p.Value)
             .HasColumnName("PhoneNumber")
             .HasMaxLength(20)
             .HasComment("User's Phone number");
        });

        // Creation Date
        builder.Property(u => u.CreationDate)
               .IsRequired()
               .HasDefaultValueSql("CURRENT_TIMESTAMP")
               .HasComment("User Creation Date");

        // Last Login Date
        builder.Property(u => u.LastLoginDate)
               .HasComment("User Last Login Date");

        // User Status (Enumeration)
        builder.Property(u => u.Status)
               .HasConversion
               (
                    s => s.Name,
                    name => UserStatus.GetAll().First(x => x.Name == name)
               )
               .HasMaxLength(32)
               .IsRequired();

        builder.Property(u => u.CompanyId);
        builder.Property(u => u.DepartmentId);
        builder.Property(u => u.WorkerPostId);

        builder.Property(u => u.EmployeeGroupIds)
               .HasColumnType("uuid[]");

        builder.Property(u => u.EmployeePrefix)
               .HasMaxLength(50);

        builder.Property(u => u.UnlimitedAccess).IsRequired();
        builder.Property(u => u.IsEmployee).IsRequired();

        builder.Property(u => u.AccessSince);

        builder.Property(u => u.AdditionalEmails)
               .HasColumnType("text[]");

        builder.Property(u => u.AdditionalPhones)
               .HasColumnType("text[]");

        builder.Property(u => u.Fax).HasMaxLength(100);
        builder.Property(u => u.Skype).HasMaxLength(100);
        builder.Property(u => u.SipNumber).HasMaxLength(100);
        builder.Property(u => u.SignatureRelativePath).HasMaxLength(500);

        // Email Confirmation Token
        builder.Property(u => u.EmailConfirmationToken)
               .HasMaxLength(256)
               .HasComment("Token for email confirmation link");

        builder.Property(u => u.EmailConfirmationTokenExpiresAt)
               .HasComment("UTC expiration time of the confirmation token");

        // Password Reset Token
        builder.Property(u => u.PasswordResetToken)
               .HasMaxLength(256)
               .HasComment("Token for password reset link");

        builder.Property(u => u.PasswordResetTokenExpiresAt)
               .HasComment("UTC expiration time of the password reset");

        builder.Navigation(u => u.UserRoles)
               .UsePropertyAccessMode(PropertyAccessMode.Field);

        // Optimistic concurrency
        builder.Property<uint>("xmin").IsRowVersion();
    }
}
using Identity.Domain.AggregatesModel.RefreshTokenAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        // Table Name
        builder.ToTable("RefreshTokens");

        // Id, AutoIncrement
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
               .ValueGeneratedOnAdd()
               .IsRequired();

        // Token Hash
        builder.Property(r => r.TokenHash)
               .IsRequired()
               .HasMaxLength(256);
        builder.HasIndex(r => r.TokenHash).IsUnique();
    }
}
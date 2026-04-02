using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Settings.Infrastructure.Persistence;

/// <summary>Design-time factory so <c>dotnet ef</c> does not boot the full API (MediatR + Data Protection).</summary>
public sealed class SettingsDbContextFactory : IDesignTimeDbContextFactory<SettingsDbContext>
{
    public SettingsDbContext CreateDbContext(string[] args)
    {
        var conn = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=SettingsDb;Username=postgres;Password=postgres";
        var options = new DbContextOptionsBuilder<SettingsDbContext>()
            .UseNpgsql(conn)
            .Options;
        return new SettingsDbContext(options);
    }
}

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Operation.Infrastructure.Persistence;

public class OperationDbContextFactory : IDesignTimeDbContextFactory<OperationDbContext>
{
    public OperationDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Operation.API");
        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
            .Build();

        var conn = config.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=OperationDb;Username=postgres;Password=postgres";

        var options = new DbContextOptionsBuilder<OperationDbContext>()
            .UseNpgsql(conn)
            .Options;

        return new OperationDbContext(options);
    }
}

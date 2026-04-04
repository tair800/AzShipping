using System.Reflection;
using AzShipping.ApiSecurity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using MrStyx.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Request.API.Authorization;
using Request.API.Extensions;
using Request.Application.Extensions;
using Request.Infrastructure.Extensions;
using Request.Infrastructure.Persistence;
using Request.Infrastructure.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(o => o.Filters.Add<RequestErpPermissionFilter>())
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProjectVersioning();
builder.Services.AddCustomSwaggerGen(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services.AddCors(o => o.AddPolicy("AllowAll", p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddErpModuleAccess(builder.Configuration);

var app = builder.Build();

// Ensure RequestDb exists
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(conn))
{
    var dbName = "RequestDb";
    var builderConn = new NpgsqlConnectionStringBuilder(conn);
    if (!string.IsNullOrEmpty(builderConn.Database))
        dbName = builderConn.Database;
    builderConn.Database = "postgres";
    try
    {
        await using var c = new NpgsqlConnection(builderConn.ConnectionString);
        await c.OpenAsync();
        await using var cmd = c.CreateCommand();
        cmd.CommandText = $"SELECT 1 FROM pg_database WHERE datname = '{dbName.Replace("'", "''")}'";
        var exists = await cmd.ExecuteScalarAsync();
        if (exists == null || exists == DBNull.Value)
        {
            await using var createCmd = c.CreateCommand();
            createCmd.CommandText = $"CREATE DATABASE \"{dbName.Replace("\"", "\"\"")}\"";
            await createCmd.ExecuteNonQueryAsync();
        }
    }
    catch { /* ignore */ }
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RequestDbContext>();
    var cmdArgs = Environment.GetCommandLineArgs();
    var removeIdx = Array.IndexOf(cmdArgs, "--remove-migration");
    if (removeIdx >= 0 && removeIdx + 1 < cmdArgs.Length)
    {
        var migrationId = cmdArgs[removeIdx + 1];
        await db.Database.ExecuteSqlRawAsync(
            "DELETE FROM \"__EFMigrationsHistory\" WHERE \"MigrationId\" = {0}", migrationId);
        Console.WriteLine($"Removed migration {migrationId} from history.");
        return;
    }
    await db.Database.MigrateAsync();
    // Ensure SubType and PackageType columns exist (fix schema mismatch if migrations were in history but not applied)
    try
    {
        await db.Database.ExecuteSqlRawAsync(@"
            DO $$ BEGIN
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_schema='public' AND table_name='RequestTypes' AND column_name='SubType') THEN
                    ALTER TABLE ""RequestTypes"" ADD COLUMN ""SubType"" character varying(50) NULL;
                END IF;
            END $$;
        ");
        await db.Database.ExecuteSqlRawAsync(@"
            DO $$ BEGIN
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_schema='public' AND table_name='RequestDimensions' AND column_name='PackageType') THEN
                    ALTER TABLE ""RequestDimensions"" ADD COLUMN ""PackageType"" character varying(50) NULL;
                END IF;
            END $$;
        ");
        await db.Database.ExecuteSqlRawAsync(@"
            DO $$ BEGIN
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_schema='public' AND table_name='Requests' AND column_name='TransitPortTerminalId') THEN
                    ALTER TABLE ""Requests"" ADD COLUMN ""TransitPortTerminalId"" uuid NULL;
                END IF;
            END $$;
        ");
        await db.Database.ExecuteSqlRawAsync(@"
            DO $$ BEGIN
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_schema='public' AND table_name='Requests' AND column_name='TransitPortName') THEN
                    ALTER TABLE ""Requests"" ADD COLUMN ""TransitPortName"" character varying(200) NULL;
                END IF;
            END $$;
        ");
        try
        {
            await db.Database.ExecuteSqlRawAsync(@"
                DO $$ BEGIN
                    IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_schema='public' AND table_name='RequestVas') THEN
                        CREATE TABLE ""RequestVas"" (
                            ""Id"" uuid NOT NULL,
                            ""RequestId"" uuid NOT NULL,
                            ""VasId"" uuid NOT NULL,
                            ""VasName"" character varying(200) NULL,
                            ""ExecutionPlace"" character varying(200) NULL,
                            ""Quantity"" numeric NOT NULL,
                            ""Uom"" character varying(100) NULL,
                            ""CurrencyId"" uuid NULL,
                            ""CurrencyCode"" character varying(10) NULL,
                            ""Total"" numeric NULL,
                            ""Notes"" character varying(1000) NULL,
                            CONSTRAINT ""PK_RequestVas"" PRIMARY KEY (""Id""),
                            CONSTRAINT ""FK_RequestVas_Requests_RequestId"" FOREIGN KEY (""RequestId"") REFERENCES ""Requests"" (""Id"") ON DELETE CASCADE
                        );
                        CREATE INDEX ""IX_RequestVas_RequestId"" ON ""RequestVas"" (""RequestId"");
                    END IF;
                END $$;
            ");
        }
        catch { /* ignore */ }
    }
    catch { /* ignore if columns already exist or other error */ }
    await RequestDbSeeder.SeedAsync(db);
}

app.UseCustomSwagger("Request");
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseErpModuleAccess();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"))
    .AllowAnonymous()
    .ExcludeFromDescription();
app.MapGet("/health", () => Results.Ok(new { service = "Request.API", status = "ok" }))
    .AllowAnonymous()
    .ExcludeFromDescription();

app.Run();

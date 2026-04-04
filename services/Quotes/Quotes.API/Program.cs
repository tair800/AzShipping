using System.Reflection;
using AzShipping.ApiSecurity;
using Microsoft.EntityFrameworkCore;
using Quotes.API.Authorization;
using Npgsql;
using Microsoft.AspNetCore.Authorization;
using Quotes.API.Extensions;
using MrStyx.API.Extensions;
using Quotes.Application.Extensions;
using Quotes.Infrastructure.Extensions;
using Quotes.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(o => o.Filters.Add<ReportErpPermissionFilter>())
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
    // Enforce auth for all endpoints by default.
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services.AddCors(o => o.AddPolicy("AllowAll", p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddErpModuleAccess(builder.Configuration);

var app = builder.Build();

// Ensure QuotesDb exists
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(conn))
{
    var dbName = "QuotesDb";
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
    var db = scope.ServiceProvider.GetRequiredService<QuotesDbContext>();
    await db.Database.MigrateAsync();

    var reseed = Environment.GetCommandLineArgs().Contains("--reseed");
    if (reseed)
    {
        await db.Quotes.ExecuteDeleteAsync();
        await db.QuoteTypes.ExecuteDeleteAsync();
        Console.WriteLine("Quotes and QuoteTypes cleared. Reseeding...");
    }

    await Quotes.Infrastructure.Persistence.Seed.QuotesDbSeeder.SeedAsync(db);
}

app.UseCustomSwagger("Quotes");
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseErpModuleAccess();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"))
    .AllowAnonymous()
    .ExcludeFromDescription();
app.MapGet("/health", () => Results.Ok(new { service = "Quotes.API", status = "ok" }))
    .AllowAnonymous()
    .ExcludeFromDescription();

app.Run();

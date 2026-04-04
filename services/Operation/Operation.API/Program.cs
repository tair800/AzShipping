using System.Reflection;
using AzShipping.ApiSecurity;
using Microsoft.AspNetCore.Authorization;
using Operation.API.Authorization;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using MrStyx.API.Extensions;
using Operation.API.Extensions;
using Operation.Application.Extensions;
using Operation.Infrastructure.Extensions;
using Operation.Infrastructure.Persistence;
using Operation.Infrastructure.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);

builder.Services.AddControllers(o => o.Filters.Add<OrderErpPermissionFilter>())
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

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(conn))
{
    var dbName = "OperationDb";
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
    var db = scope.ServiceProvider.GetRequiredService<OperationDbContext>();
    await db.Database.MigrateAsync();
    await OperationDbSeeder.SeedAsync(db);
    await OperationDbSeeder.EnsureExportAirMultimodalTypesAsync(db);
    await OperationDbSeeder.EnsureImportAirTypesAsync(db);
    await OperationDbSeeder.EnsureTransitAirTypesAsync(db);
    await OperationDbSeeder.EnsureDomesticAirTypesAsync(db);
    await OperationDbSeeder.EnsureExportSeaFclTypesAsync(db);
    await OperationDbSeeder.EnsureImportSeaFclTypesAsync(db);
    await OperationDbSeeder.EnsureImportSeaLclTypesAsync(db);
    await OperationDbSeeder.EnsureExportSeaLclTypesAsync(db);
    await OperationDbSeeder.EnsureExportSeaBreakbulkTypesAsync(db);
    await OperationDbSeeder.EnsureExportRailFclTypesAsync(db);
    await OperationDbSeeder.EnsureExportRailLclTypesAsync(db);
    await OperationDbSeeder.EnsureExportRailBreakbulkTypesAsync(db);
    await OperationDbSeeder.EnsureImportRailFclTypesAsync(db);
    await OperationDbSeeder.EnsureImportRailLclTypesAsync(db);
    await OperationDbSeeder.EnsureImportRailBreakbulkTypesAsync(db);
    await OperationDbSeeder.EnsureTransitRailFclTypesAsync(db);
    await OperationDbSeeder.EnsureTransitRailLclTypesAsync(db);
    await OperationDbSeeder.EnsureTransitRailBreakbulkTypesAsync(db);
    await OperationDbSeeder.EnsureDomesticRailFclTypesAsync(db);
    await OperationDbSeeder.EnsureImportSeaBreakbulkTypesAsync(db);
    await OperationDbSeeder.EnsureTransitSeaFclTypesAsync(db);
    await OperationDbSeeder.EnsureTransitSeaLclTypesAsync(db);
    await OperationDbSeeder.EnsureTransitSeaBreakbulkTypesAsync(db);
    await OperationDbSeeder.EnsureDomesticSeaFclTypesAsync(db);
    await OperationDbSeeder.EnsureExportRoadFtlTypesAsync(db);
    await OperationDbSeeder.EnsureImportRoadFtlTypesAsync(db);
    await OperationDbSeeder.EnsureImportRoadLtlTypesAsync(db);
    await OperationDbSeeder.EnsureImportRoadBreakbulkTypesAsync(db);
    await OperationDbSeeder.EnsureImportRoadOogTypesAsync(db);
    await OperationDbSeeder.EnsureExportRoadLtlTypesAsync(db);
    await OperationDbSeeder.EnsureExportRoadBreakbulkTypesAsync(db);
    await OperationDbSeeder.EnsureExportRoadOogTypesAsync(db);
    await OperationDbSeeder.EnsureTransitRoadFtlTypesAsync(db);
    await OperationDbSeeder.EnsureTransitRoadLtlTypesAsync(db);
    await OperationDbSeeder.EnsureTransitRoadBreakbulkTypesAsync(db);
    await OperationDbSeeder.EnsureTransitRoadOogTypesAsync(db);
    await OperationDbSeeder.EnsureDomesticRoadFtlTypesAsync(db);
    await OperationDbSeeder.EnsureDomesticRoadLtlTypesAsync(db);
    await OperationDbSeeder.EnsureDomesticRoadBreakbulkTypesAsync(db);
    await OperationDbSeeder.EnsureDomesticRoadOogTypesAsync(db);
    await OperationDbSeeder.SeedDemoLogisticsOperationsAsync(db);
}

app.UseCustomSwagger("Operation");
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseErpModuleAccess();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"))
    .AllowAnonymous()
    .ExcludeFromDescription();
app.MapGet("/health", () => Results.Ok(new { service = "Operation.API", status = "ok" }))
    .AllowAnonymous()
    .ExcludeFromDescription();

app.Run();

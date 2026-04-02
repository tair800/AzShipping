using System.Reflection;
using AzShipping.ApiSecurity;
using General.Application.Extensions;
using General.Infrastructure.Extensions;
using General.Infrastructure.Persistence;
using MrStyx.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using General.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);

builder.Services.AddHttpContextAccessor();
builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(o =>
    o.MultipartBodyLengthLimit = 52_428_800);
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);
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
builder.Services.AddCors(o => o.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Ensure GeneralDb exists
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(conn))
{
    var dbName = "GeneralDb";
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
    var db = scope.ServiceProvider.GetRequiredService<GeneralDbContext>();
    await db.Database.MigrateAsync();
    await General.Infrastructure.Persistence.Seed.GeneralDbSeeder.SeedAsync(db);
}

app.UseCustomSwagger("General");
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseErpModuleAccess();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"))
    .AllowAnonymous()
    .ExcludeFromDescription();
app.Run();

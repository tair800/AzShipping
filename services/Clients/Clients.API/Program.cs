using System.Reflection;
using AzShipping.ApiSecurity;
using Clients.API.Authorization;
using Clients.API.Extensions;
using Clients.Application.Extensions;
using Clients.Infrastructure.Extensions;
using Clients.Infrastructure.Persistence;
using Clients.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using MrStyx.API.Extensions;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(o => o.Filters.Add<ClientErpPermissionFilter>())
    .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);
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
builder.Services.AddErpModuleAccess(builder.Configuration);

var app = builder.Build();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(conn))
{
    var dbName = "ClientsDb";
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
    catch { /* ignore - migration may still work if db exists */ }
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClientsDbContext>();
    await db.Database.MigrateAsync();
    await ClientsDbSeeder.SeedAsync(db);
}

app.Use(async (ctx, next) =>
{
    try { await next(); }
    catch (Exception ex)
    {
        ctx.Response.StatusCode = 500;
        ctx.Response.ContentType = "application/json";
        var inner = ex.InnerException?.Message ?? ex.InnerException?.InnerException?.Message;
        await ctx.Response.WriteAsJsonAsync(new { error = ex.Message, detail = inner, stack = ex.StackTrace });
    }
});
app.UseCustomSwagger("Clients");
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseErpModuleAccess();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"))
    .AllowAnonymous()
    .ExcludeFromDescription();
app.Run();

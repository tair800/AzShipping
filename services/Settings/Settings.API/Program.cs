using System.Reflection;
using AzShipping.ApiSecurity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Settings.Application.Extensions;
using Settings.Infrastructure.Extensions;
using Settings.Infrastructure.Logging;
using Settings.API.Authorization;
using Settings.API.Extensions;
using Settings.API.Options;
using MrStyx.API.Extensions;
using Microsoft.AspNetCore.Authorization;

Console.WriteLine("[Settings] Starting...");
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);

builder.Host.UseSerilog((context, services, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "Settings.API")
        .WriteTo.Console()
        .WriteTo.Sink(new SystemLogSink(services.GetRequiredService<IServiceScopeFactory>()));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(o => o.Filters.Add<SettingsErpPermissionFilter>())
    .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProjectVersioning();
builder.Services.AddCustomSwaggerGen(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<EmailSystemSendOptions>(builder.Configuration.GetSection(EmailSystemSendOptions.SectionName));
builder.Services.Configure<EmployeeGroupResolveOptions>(builder.Configuration.GetSection(EmployeeGroupResolveOptions.SectionName));
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services.AddCors(o => o.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddErpModuleAccess(builder.Configuration);

Console.WriteLine("[Settings] Building application...");
var app = builder.Build();

Console.WriteLine("[Settings] Migrating database...");
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<Settings.Infrastructure.Persistence.SettingsDbContext>();
    var migrateTask = db.Database.MigrateAsync();
    if (await Task.WhenAny(migrateTask, Task.Delay(15000)) != migrateTask)
        throw new TimeoutException("Database connection timed out after 15s. Is PostgreSQL running?");
    await migrateTask;
    await Settings.Infrastructure.Persistence.Seed.SettingsDbSeeder.SeedAsync(db);
    Console.WriteLine("[Settings] Database ready.");
}
catch (TimeoutException ex)
{
    Console.WriteLine($"[Settings] ERROR: {ex.Message}");
    throw;
}
catch (Exception ex)
{
    Console.WriteLine($"[Settings] ERROR: Database failed: {ex.Message}");
    throw;
}

app.UseCustomSwagger("Settings");
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseErpModuleAccess();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"))
    .AllowAnonymous()
    .ExcludeFromDescription();
Console.WriteLine("[Settings] Listening on http://localhost:5064");
try
{
    app.Run();
}
finally
{
    await Log.CloseAndFlushAsync();
}

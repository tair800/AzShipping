using Identity.API.Extensions;
using Identity.Application.Extensions;
using Identity.Infrastructure.Extensions;
using MrStyx.API.Extensions;
using MrStyx.API.Middlewares;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomSerilog(builder.Configuration);

builder.Services.AddCustomControllers();
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    options.JsonSerializerOptions.AllowTrailingCommas = true;
});

builder.Services.AddProjectVersioning();

builder.Services.AddCustomSwaggerGen(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddCustomHttpContextAccessor();

builder.Services.AddPermissions();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

app.Services.MigrateDatabase();

await app.UseSeedDataAsync();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCustomSwagger("Identity");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
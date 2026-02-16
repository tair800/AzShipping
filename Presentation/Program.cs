using AzShipping.Application.Services;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using AzShipping.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AzShipping API",
        Version = "v1",
        Description = "Logistic Backend API"
    });
});

// Configure Entity Framework with PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? "Host=localhost;Port=5432;Database=AzShippingDb;Username=postgres;Password=12345@Tt";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register repositories
builder.Services.AddScoped<IClientSegmentRepository, ClientSegmentRepository>();
builder.Services.AddScoped<IRequestSourceRepository, RequestSourceRepository>();
builder.Services.AddScoped<IPackagingRepository, PackagingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISalesFunnelStatusRepository, SalesFunnelStatusRepository>();
builder.Services.AddScoped<ITransportTypeRepository, TransportTypeRepository>();

// Register services
builder.Services.AddScoped<IClientSegmentService, ClientSegmentService>();
builder.Services.AddScoped<IRequestSourceService, RequestSourceService>();
builder.Services.AddScoped<IPackagingService, PackagingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISalesFunnelStatusService, SalesFunnelStatusService>();
builder.Services.AddScoped<ITransportTypeService, TransportTypeService>();

var app = builder.Build();

// Ensure database is created and seed initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    DatabaseSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AzShipping API v1");
    });
}

// Use HTTPS redirection only if HTTPS is configured
var urls = builder.Configuration["ASPNETCORE_URLS"] ?? 
           Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "";
if (urls.Contains("https://", StringComparison.OrdinalIgnoreCase))
{
    app.UseHttpsRedirection();
}

// Enable CORS
app.UseCors("AllowAll");

app.UseAuthorization();

// Enable static files (for frontend UI)
app.UseStaticFiles();

app.MapControllers();

// Default route - serve the frontend UI
app.MapGet("/", () => Results.Redirect("/index.html"));

app.Run();

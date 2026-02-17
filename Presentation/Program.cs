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
builder.Services.AddScoped<ILoadingMethodRepository, LoadingMethodRepository>();
builder.Services.AddScoped<IDeferredPaymentConditionRepository, DeferredPaymentConditionRepository>();
builder.Services.AddScoped<IRequestPurposeRepository, RequestPurposeRepository>();
builder.Services.AddScoped<IDrivingLicenceCategoryRepository, DrivingLicenceCategoryRepository>();
builder.Services.AddScoped<IWorkerPostRepository, WorkerPostRepository>();
builder.Services.AddScoped<ICarrierTypeRepository, CarrierTypeRepository>();
builder.Services.AddScoped<IBankRepository, BankRepository>();

// Register services
builder.Services.AddScoped<IClientSegmentService, ClientSegmentService>();
builder.Services.AddScoped<IRequestSourceService, RequestSourceService>();
builder.Services.AddScoped<IPackagingService, PackagingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISalesFunnelStatusService, SalesFunnelStatusService>();
builder.Services.AddScoped<ITransportTypeService, TransportTypeService>();
builder.Services.AddScoped<ILoadingMethodService, LoadingMethodService>();
builder.Services.AddScoped<IDeferredPaymentConditionService, DeferredPaymentConditionService>();
builder.Services.AddScoped<IRequestPurposeService, RequestPurposeService>();
builder.Services.AddScoped<IDrivingLicenceCategoryService, DrivingLicenceCategoryService>();
builder.Services.AddScoped<IWorkerPostService, WorkerPostService>();
builder.Services.AddScoped<ICarrierTypeService, CarrierTypeService>();
builder.Services.AddScoped<BankService>();

var app = builder.Build();

// Ensure database is created and seed initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    
    // Ensure LoadingMethod tables exist (for existing databases)
    // This handles the case where the database already exists but LoadingMethod tables don't
    try
    {
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""LoadingMethods"" (
                ""Id"" UUID PRIMARY KEY,
                ""IsActive"" BOOLEAN NOT NULL,
                ""CreatedAt"" TIMESTAMP NOT NULL,
                ""UpdatedAt"" TIMESTAMP
            );
        ");
        
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""LoadingMethodTranslations"" (
                ""Id"" UUID PRIMARY KEY,
                ""LoadingMethodId"" UUID NOT NULL,
                ""LanguageCode"" VARCHAR(10) NOT NULL,
                ""Name"" VARCHAR(200) NOT NULL,
                CONSTRAINT ""FK_LoadingMethodTranslations_LoadingMethods_LoadingMethodId"" 
                    FOREIGN KEY (""LoadingMethodId"") 
                    REFERENCES ""LoadingMethods"" (""Id"") 
                    ON DELETE CASCADE
            );
        ");
        
        context.Database.ExecuteSqlRaw(@"
            CREATE UNIQUE INDEX IF NOT EXISTS ""IX_LoadingMethodTranslations_LoadingMethodId_LanguageCode"" 
                ON ""LoadingMethodTranslations"" (""LoadingMethodId"", ""LanguageCode"");
        ");
        
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""DeferredPaymentConditions"" (
                ""Id"" UUID PRIMARY KEY,
                ""Name"" VARCHAR(200) NOT NULL,
                ""FullText"" VARCHAR(1000),
                ""IsActive"" BOOLEAN NOT NULL,
                ""CreatedAt"" TIMESTAMP NOT NULL,
                ""UpdatedAt"" TIMESTAMP
            );
        ");
        
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""RequestPurposes"" (
                ""Id"" UUID PRIMARY KEY,
                ""Name"" VARCHAR(200) NOT NULL,
                ""IsActive"" BOOLEAN NOT NULL,
                ""CreatedAt"" TIMESTAMP NOT NULL,
                ""UpdatedAt"" TIMESTAMP
            );
        ");
        
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""DrivingLicenceCategories"" (
                ""Id"" UUID PRIMARY KEY,
                ""Name"" VARCHAR(10) NOT NULL,
                ""IsActive"" BOOLEAN NOT NULL,
                ""CreatedAt"" TIMESTAMP NOT NULL,
                ""UpdatedAt"" TIMESTAMP
            );
        ");
        
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""WorkerPosts"" (
                ""Id"" UUID PRIMARY KEY,
                ""IsActive"" BOOLEAN NOT NULL,
                ""CreatedAt"" TIMESTAMP NOT NULL,
                ""UpdatedAt"" TIMESTAMP
            );
        ");
        
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""WorkerPostTranslations"" (
                ""Id"" UUID PRIMARY KEY,
                ""WorkerPostId"" UUID NOT NULL,
                ""LanguageCode"" VARCHAR(10) NOT NULL,
                ""Name"" VARCHAR(200) NOT NULL,
                CONSTRAINT ""FK_WorkerPostTranslations_WorkerPosts_WorkerPostId"" 
                    FOREIGN KEY (""WorkerPostId"") 
                    REFERENCES ""WorkerPosts"" (""Id"") 
                    ON DELETE CASCADE
            );
        ");
        
        context.Database.ExecuteSqlRaw(@"
            CREATE UNIQUE INDEX IF NOT EXISTS ""IX_WorkerPostTranslations_WorkerPostId_LanguageCode"" 
                ON ""WorkerPostTranslations"" (""WorkerPostId"", ""LanguageCode"");
        ");
        
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""CarrierTypes"" (
                ""Id"" UUID PRIMARY KEY,
                ""Name"" VARCHAR(200) NOT NULL,
                ""IsActive"" BOOLEAN NOT NULL,
                ""Colour"" VARCHAR(7) NOT NULL,
                ""IsDefault"" BOOLEAN NOT NULL,
                ""CreatedAt"" TIMESTAMP NOT NULL,
                ""UpdatedAt"" TIMESTAMP
            );
        ");
        
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""Banks"" (
                ""Id"" UUID PRIMARY KEY,
                ""Name"" VARCHAR(200) NOT NULL,
                ""UnofficialName"" VARCHAR(200),
                ""Branch"" VARCHAR(200),
                ""Code"" VARCHAR(50),
                ""Swift"" VARCHAR(50),
                ""Country"" VARCHAR(100),
                ""City"" VARCHAR(100),
                ""Address"" VARCHAR(500),
                ""PostCode"" VARCHAR(20),
                ""CreatedAt"" TIMESTAMP NOT NULL,
                ""UpdatedAt"" TIMESTAMP
            );
        ");
    }
    catch
    {
        // Tables might already exist or there was an error, continue anyway
        // The seeder will handle inserting data if tables are empty
    }
    
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

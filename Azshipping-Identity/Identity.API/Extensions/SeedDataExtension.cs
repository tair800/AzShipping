using Identity.Application.Interfaces.Services;
using Identity.Infrastructure.Persistence;

namespace Identity.API.Extensions;

public static class SeedDataExtension
{
    public static async Task<IApplicationBuilder> UseSeedDataAsync(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordService>();

            await AppDataSeeder.SeedAsync(scope.ServiceProvider, passwordService);
        }

        return app;
    }
}
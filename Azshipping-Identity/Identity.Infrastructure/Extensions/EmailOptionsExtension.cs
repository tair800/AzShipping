using Identity.Application.Interfaces.Services;
using Identity.Infrastructure.Options;
using Identity.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Extensions;

public static class EmailOptionsExtension
{
    public static IServiceCollection AddEmailOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<EmailOptions>()
                .Bind(configuration.GetSection(EmailOptions.SectionName))
                .Validate(o => !string.IsNullOrWhiteSpace(o.Host), "Email:Host is required")
                .Validate(o => o.Port >= 1 && o.Port <= 65535, "Email:Port must be between 1 and 65535")
                .Validate(o => !string.IsNullOrWhiteSpace(o.From), "Email:From is required")
                .Validate(o => o.ConfirmationTokenLifeTimeHours >= 1 && o.ConfirmationTokenLifeTimeHours <= 168,
                    "Email:ConfirmationTokenLifeTimeHours must be between 1 and 168 (1 week)")
                .ValidateOnStart();

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
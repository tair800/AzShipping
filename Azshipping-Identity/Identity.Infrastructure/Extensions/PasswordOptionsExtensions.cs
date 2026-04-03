using Identity.Application.Interfaces.Services;
using Identity.Infrastructure.Options;
using Identity.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Extensions;

public static class PasswordOptionsExtensions
{
    public static IServiceCollection AddPasswordOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<Argon2Options>()
            .Bind(configuration.GetSection("Argon2"))
            .Validate(s => s.Iterations >= 1 && s.Iterations <= 10000, "Iterations must be 1..10000")
            .Validate(s => s.MemorySize >= 32768 && s.MemorySize <= 1048576, "MemorySize(KB) must be 32_768..1_048_576")
            .Validate(s => s.DegreeOfParallelism >= 1 && s.DegreeOfParallelism <= Environment.ProcessorCount, "DegreeOfParallelism out of range")
            .Validate(s => s.SaltLength >= 16 && s.SaltLength <= 64, "SaltLength must be 16..64")
            .Validate(s => s.HashLength >= 16 && s.HashLength <= 64, "HashLength must be 16..64")
            .ValidateOnStart();

        var pepperB64 = configuration["Security:PasswordPepper"];
        var pepperVersionString = configuration["Security:PasswordPepperVersion"];
        var pepperBytes = Convert.FromBase64String(pepperB64!);
        var pepperVersion = int.TryParse(pepperVersionString, out var v) ? v : 1;

        var pepperOptions = new PepperOptions { Pepper = pepperBytes, Version = pepperVersion };

        services.AddSingleton(pepperOptions);

        services.AddSingleton<IPasswordService, PasswordService>();

        return services;
    }
}
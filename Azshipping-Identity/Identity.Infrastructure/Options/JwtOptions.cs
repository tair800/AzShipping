namespace Identity.Infrastructure.Options;

public class JwtOptions
{
    public string Issuer { get; init; } = "IdentityService";
    public string Audience { get; init; } = "IdentityService.Clients";
    public int AccessTokenLifetimeMinutes { get; init; } = 30;
}
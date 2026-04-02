namespace Operation.API.Options;

public class JwtOptions
{
    public const string SectionName = "JWT";
    public string Issuer { get; init; } = "IdentityService";
    public string Audience { get; init; } = "IdentityService.Clients";
}

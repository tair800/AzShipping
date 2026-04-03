namespace Identity.Application.DTOs.Auth;

public sealed record AccessTokenDto
{
    public required string Token { get; init; }
    public string TokenType { get; init; } = "Bearer";
    public DateTime IssuedAtUtc { get; init; }
    public DateTime ExpiresAtUtc { get; init; }
    public int ExpiresInSeconds => (int)(ExpiresAtUtc - IssuedAtUtc).TotalSeconds;
}

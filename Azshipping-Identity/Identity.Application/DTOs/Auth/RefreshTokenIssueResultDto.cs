namespace Identity.Application.DTOs.Auth;

public sealed record RefreshTokenIssueResultDto(string RefreshToken, DateTime RefreshTokenExpiresAtUtc);

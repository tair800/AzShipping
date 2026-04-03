namespace Identity.Application.DTOs.Auth;

public sealed record RevokeRefreshTokenRequestDto(string RefreshToken, bool RevokeFamily = false);

namespace Identity.Application.DTOs.Auth;

public sealed record RefreshResultDto
(
    AccessTokenDto AccessToken,

    string RefreshToken,

    DateTime RefreshTokenExpiresAtUtc
);

using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Identity.Application.DTOs.Auth;
using Identity.Application.Features.Auth.Commands.Login;
using Identity.Application.Features.Auth.Commands.IssueRefreshToken;
using Identity.Application.Features.Auth.Commands.RefreshToken;
using Identity.Application.Features.Auth.Commands.RevokeRefreshToken;
using Identity.Application.Features.Auth.Commands.RevokeAllRefreshTokens;
using Identity.Application.Features.Auth.Commands.ConfirmEmail;
using Identity.Application.Features.Auth.Commands.ForgotPassword;
using Identity.Application.Features.Auth.Commands.ResetPassword;
using MrStyx.API.Common;

namespace Identity.API.Controllers.V1;


[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("login")]
    public async Task<ApiResponse<AccessTokenDto>> LoginAsync([FromBody] LoginDto dto)
    {
        var result = await _mediator.Send(new LoginCommand(dto));
        return ApiResponse<AccessTokenDto>.Ok(result, "Login Successful", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPost("refresh/issue")]
    [Authorize]
    public async Task<ApiResponse<RefreshTokenIssueResultDto>> IssueRefreshAsync()
    {
        var userId = Convert.ToInt64(User.FindFirst("uid")?.Value);

        var result = await _mediator.Send(new IssueRefreshTokenCommand(userId));
        return ApiResponse<RefreshTokenIssueResultDto>.Ok(result, "Refresh token issued", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPost("refresh")]
    public async Task<ApiResponse<RefreshResultDto>> RefreshAsync([FromBody] string refreshToken)
    {
        var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));
        return ApiResponse<RefreshResultDto>.Ok(result, "Token refreshed", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPost("refresh/revoke")]
    public async Task<ApiResponse> RevokeAsync([FromBody] RevokeRefreshTokenRequestDto dto)
    {
        await _mediator.Send(new RevokeRefreshTokenCommand(dto));
        return ApiResponse.Ok("Refresh token revoked", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPost("refresh/revoke-all")]
    [Authorize]
    public async Task<ApiResponse> RevokeAllAsync()
    {
        var userId = Convert.ToInt64(User.FindFirst("uid")?.Value);

        await _mediator.Send(new RevokeAllRefreshTokensCommand(userId));
        return ApiResponse.Ok("All refresh tokens revoked", traceId: HttpContext.TraceIdentifier);
    }

    [HttpGet("confirm-email")]
    public async Task<ApiResponse> ConfirmEmailAsync([FromQuery] string token)
    {
        await _mediator.Send(new ConfirmEmailCommand(token));
        return ApiResponse.Ok("Account confirmed. You can now log in", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPost("forgot-password")]
    public async Task<ApiResponse> ForgotPasswordAsync(string email)
    {
        await _mediator.Send(new ForgotPasswordCommand(email));
        return ApiResponse.Ok("Check your email — we’ve sent you a password reset link. If you don’t see it, check your Spam folder or try again in a couple of minutes.", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPost("reset-password")]
    public async Task<ApiResponse> ResetPasswordAsync([FromBody] ResetPasswordDto dto)
    {
        await _mediator.Send(new ResetPasswordCommand(dto.Token, dto.NewPassword));
        return ApiResponse.Ok("Password changed successfully", traceId: HttpContext.TraceIdentifier);
    }
}
using System.Text;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Identity.Application.DTOs.User;
using Identity.Application.Features.Users.Queries.GetAll;
using Identity.Application.Features.Users.Queries.GetById;
using Identity.Application.Features.Users.Commands.Create;
using Identity.Application.Features.Users.Commands.Update;
using Identity.Application.Features.Users.Commands.Delete;
using Identity.Application.Features.Users.Commands.UpdateStatus;
using Identity.Application.Features.Users.Queries.GetPaged;
using Identity.Application.Features.Users.Queries.GetWhere;
using Identity.Application.Features.Users.Queries.GetPagedWhere;
using Identity.Domain.AggregatesModel.UserAggregate.Enumerations;
using Identity.Application.Features.Users.Queries.GetStatus;
using Identity.Application.Features.Users.Queries.GetLicenseStats;
using Identity.Application.Features.Users.Commands.UploadSignature;
using MrStyx.API.Common;
using MrStyx.Application.Exceptions;

namespace Identity.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
public class UsersController(IMediator mediator, ILogger<UsersController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<UsersController> _logger = logger;

    [HttpGet("get/all")]
    public async Task<ApiResponse<UserList>> GetAllUsersAsync()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return ApiResponse<UserList>.Ok(result, "Users retrieved successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpGet("get/by/{id}")]
    public async Task<ApiResponse<UserDto>> GetUserByIdAsync(long id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return ApiResponse<UserDto>.Ok(result, "User retrieved successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPost("create")]
    public async Task<ApiResponse<UserDto>> CreateUserAsync([FromBody] CreateUserDto? dto)
    {
        if (dto is null)
        {
            await LogCreateUserBindingFailureAsync();
            var clientMsg = SummarizeModelStateForClient(ModelState);
            throw string.IsNullOrEmpty(clientMsg)
                ? new BadRequestException("Request body is missing or is not valid JSON for create user.")
                : new BadRequestException(clientMsg);
        }

        _logger.LogInformation(
            "HTTP CreateUser: Username={Username}, Email={Email}, RoleCount={RoleCount}, TraceId={TraceId}",
            dto.Username,
            dto.Email,
            dto.RoleIds?.Count ?? 0,
            HttpContext.TraceIdentifier);

        var result = await _mediator.Send(new CreateUserCommand(dto));
        return ApiResponse<UserDto>.Ok(result, "User created successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPut("update")]
    public async Task<ApiResponse<UserDto>> UpdateUserAsync(UpdateUserDto dto)
    {
        var result = await _mediator.Send(new UpdateUserCommand(dto));
        return ApiResponse<UserDto>.Ok(result, "User updated successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPut("update/status")]
    public async Task<ApiResponse<UserDto>> UpdateUserStatusAsync([FromBody] UpdateUserStatusDto dto)
    {
        var result = await _mediator.Send(new UpdateUserStatusCommand(dto));
        return ApiResponse<UserDto>.Ok(result, "User status updated successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ApiResponse<bool>> DeleteUserAsync(long id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id));
        return ApiResponse<bool>.Ok(result, "User deleted successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpGet("get/where")]
    public async Task<ApiResponse<UserList>> GetWhereAsync([FromQuery] SearchUserDto dto)
    {
        var result = await _mediator.Send(new GetUsersWhereQuery(dto));
        return ApiResponse<UserList>.Ok(result, "Users retrieved successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpGet("get/paged")]
    public async Task<ApiResponse<UserList>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var result = await _mediator.Send(new GetPagedUsersQuery(pageNumber, pageSize));
        return ApiResponse<UserList>.Ok(result.Items, "Users retrieved successfully", traceId: HttpContext.TraceIdentifier, meta: result.Meta);
    }

    [HttpGet("get/paged/where")]
    public async Task<ApiResponse<UserList>> GetPagedWhereAsync(int pageNumber, int pageSize, [FromQuery] SearchUserDto dto)
    {
        var result = await _mediator.Send(new GetPagedUsersWhereQuery(pageNumber, pageSize, dto));
        return ApiResponse<UserList>.Ok(result.Items, "Users retrieved successfully", traceId: HttpContext.TraceIdentifier, meta: result.Meta);
    }

    [HttpGet("get/statuses")]
    public async Task<ApiResponse<IReadOnlyCollection<UserStatus>>> GetStatusesAsync()
    {
        var result = await _mediator.Send(new GetUserStatusesQuery());
        return ApiResponse<IReadOnlyCollection<UserStatus>>.Ok(result, "User's statuses retrieved succesfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpGet("get/licenses")]
    public async Task<ApiResponse<UserLicenseStatsDto>> GetLicenseStatsAsync()
    {
        var result = await _mediator.Send(new GetUserLicenseStatsQuery());
        return ApiResponse<UserLicenseStatsDto>.Ok(result, "License stats retrieved successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPost("{id:long}/signature")]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(5_000_000)]
    public async Task<ApiResponse<string>> UploadSignatureAsync(long id, IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            throw new BadRequestException("Signature file is required");

        await using var stream = file.OpenReadStream();
        var relativePath = await _mediator.Send(new UploadUserSignatureCommand(id, stream, file.FileName), cancellationToken);
        return ApiResponse<string>.Ok(relativePath, "Signature uploaded successfully", traceId: HttpContext.TraceIdentifier);
    }

    /// <summary>Human-readable binder/deserialization errors (e.g. invalid Guid in employeeGroupIds).</summary>
    private static string? SummarizeModelStateForClient(ModelStateDictionary modelState)
    {
        if (modelState.ErrorCount == 0)
            return null;

        static string? Message(ModelError e)
        {
            if (!string.IsNullOrWhiteSpace(e.ErrorMessage))
                return e.ErrorMessage.Trim();
            return e.Exception?.Message.Trim();
        }

        var parts = modelState
            .Where(kv => kv.Value is { Errors.Count: > 0 })
            .SelectMany(kv => kv.Value!.Errors.Select(e => Message(e)).Where(m => !string.IsNullOrEmpty(m)).Select(m => $"{kv.Key}: {m}"))
            .ToList();

        return parts.Count == 0 ? null : string.Join(' ', parts);
    }

    private async Task LogCreateUserBindingFailureAsync()
    {
        try
        {
            HttpContext.Request.EnableBuffering();
            HttpContext.Request.Body.Position = 0;
            using var reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true);
            var raw = await reader.ReadToEndAsync();
            HttpContext.Request.Body.Position = 0;

            var modelErrors = ModelState.Count == 0
                ? "(none)"
                : string.Join("; ", ModelState.SelectMany(kv => kv.Value!.Errors.Select(e =>
                    $"{kv.Key}:{e.ErrorMessage}" + (e.Exception != null ? $"({e.Exception.GetType().Name}:{e.Exception.Message})" : ""))));

            var preview = raw.Length <= 2048 ? raw : raw[..2048] + "...";

            _logger.LogWarning(
                "HTTP CreateUser: CreateUserDto is null (body missing or JSON did not match). TraceId={TraceId}, ContentType={ContentType}, DeclaredContentLength={ContentLength}, BodyLength={BodyLen}, ModelState={ModelErrors}, BodyPreview={Preview}",
                HttpContext.TraceIdentifier,
                HttpContext.Request.ContentType,
                HttpContext.Request.ContentLength,
                raw.Length,
                modelErrors,
                preview);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "HTTP CreateUser: diagnostic read of request body failed. TraceId={TraceId}", HttpContext.TraceIdentifier);
        }
    }
}
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Infrastructure.Services;

public sealed class GeneralEmployeeProvisioningService(
    IHttpClientFactory httpClientFactory,
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository,
    IPermissionReadService permissionReadService,
    IEmployeeGroupPermissionClaimsService employeeGroupPermissionClaimsService,
    ITokenService tokenService,
    IOptions<GeneralClientOptions> options,
    ILogger<GeneralEmployeeProvisioningService> logger)
    : IGeneralEmployeeProvisioningService
{
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public async Task TryProvisionEmployeeAsync(
        long identityUserId,
        string username,
        string? fullName,
        string email,
        string? phone,
        Guid? departmentId,
        Guid? workerPostId,
        CancellationToken cancellationToken)
    {
        var opt = options.Value;
        if (!opt.Enabled || string.IsNullOrWhiteSpace(opt.BaseUrl))
        {
            logger.LogDebug("General employee provisioning skipped (disabled or empty BaseUrl).");
            return;
        }

        var baseUrl = opt.BaseUrl.TrimEnd('/');
        var client = httpClientFactory.CreateClient();
        try
        {
            var bodyObj = new
            {
                userId = identityUserId,
                fullName,
                username,
                departmentId,
                workerPostId,
                email,
                phone
            };
            var json = JsonSerializer.Serialize(bodyObj, JsonOpts);
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/api/employees")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            await AttachBearerAsync(request, cancellationToken);

            var response = await client.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation(
                    "General.API employee created for Identity UserId={UserId}, Username={Username}",
                    identityUserId,
                    username);
                return;
            }

            var err = await response.Content.ReadAsStringAsync(cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                logger.LogWarning(
                    "General.API returned 401 for employee create (UserId={UserId}). " +
                    "General requires a JWT: log in on the create request (e.g. identity-users.html / BFF), " +
                    "or set General:ProvisioningActAsUsername (e.g. admin) in Identity appsettings.Development.json. Body: {Body}",
                    identityUserId,
                    err.Length > 400 ? err[..400] + "..." : err);
            }
            else
            {
                logger.LogWarning(
                    "General.API employee create failed for UserId={UserId}: HTTP {Status}. Body: {Body}",
                    identityUserId,
                    (int)response.StatusCode,
                    err.Length > 500 ? err[..500] + "..." : err);
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(
                ex,
                "General.API employee create threw for UserId={UserId}. Ensure General is running at {BaseUrl}.",
                identityUserId,
                baseUrl);
        }
    }

    private async Task AttachBearerAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var http = httpContextAccessor.HttpContext;
        if (http?.Request.Headers.TryGetValue("Authorization", out var authHeader) == true)
        {
            var auth = authHeader.ToString();
            if (!string.IsNullOrWhiteSpace(auth))
            {
                if (auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    var token = auth["Bearer ".Length..].Trim();
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                else
                    request.Headers.TryAddWithoutValidation("Authorization", auth);

                return;
            }
        }

        var actAs = options.Value.ProvisioningActAsUsername?.Trim();
        if (string.IsNullOrEmpty(actAs))
            return;

        var user = await userRepository.GetFirstOrDefaultAsync(
            u => u.Username.Value == actAs,
            cancellationToken,
            trackingMode: QueryTrackingMode.NoTracking);

        if (user is null)
        {
            logger.LogWarning(
                "General provisioning: ProvisioningActAsUsername={Name} not found in Identity. Cannot call General without caller Bearer token.",
                actAs);
            return;
        }

        var roles = await permissionReadService.GetUserRolesAsync(user.Id);
        var perms = await permissionReadService.GetUserPermissionsAsync(user.Id);
        var erp = await employeeGroupPermissionClaimsService.ResolveAsync(user.EmployeeGroupIds, user.UnlimitedAccess, cancellationToken);
        var access = tokenService.GenerateAccessToken(user.Id, user.Username.Value, user.Email.Value, roles, perms, erp);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access.Token);
        logger.LogInformation(
            "General employee provisioning: using JWT for fallback user {Username} (no Authorization on create request).",
            actAs);
    }
}

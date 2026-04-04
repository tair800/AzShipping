using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Identity.Application.Interfaces.Services;
using Identity.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Services;

public sealed class EmailSettingsMailboxLinker(
    IHttpClientFactory httpClientFactory,
    IHttpContextAccessor httpContextAccessor,
    IOptions<SettingsClientOptions> options,
    ILogger<EmailSettingsMailboxLinker> logger) : IEmailSettingsMailboxLinker
{
    public async Task TryLinkMailboxAsync(
        Guid emailSettingId,
        long identityUserId,
        string? linkedUserDisplayName,
        CancellationToken cancellationToken = default)
    {
        var baseUrl = options.Value.BaseUrl.TrimEnd('/');
        var client = httpClientFactory.CreateClient();
        using var request = new HttpRequestMessage(
            HttpMethod.Patch,
            $"{baseUrl}/api/email-settings/{emailSettingId:D}/link-identity-user");
        AttachAuth(request);
        request.Content = JsonContent.Create(
            new { identityUserId, linkedUserDisplayName },
            options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        try
        {
            var response = await client.SendAsync(request, cancellationToken);
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning(
                    "Link mailbox to user failed: EmailSettingId={EmailSettingId} UserId={UserId} Status={Status} Body={Body}",
                    emailSettingId,
                    identityUserId,
                    (int)response.StatusCode,
                    body.Length > 500 ? body[..500] : body);
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex,
                "Link mailbox to user threw: EmailSettingId={EmailSettingId} UserId={UserId}",
                emailSettingId,
                identityUserId);
        }
    }

    private void AttachAuth(HttpRequestMessage request)
    {
        if (httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Authorization", out var authValues) != true)
            return;
        var auth = authValues.ToString();
        if (string.IsNullOrWhiteSpace(auth)) return;
        if (auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            var token = auth["Bearer ".Length..].Trim();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
            request.Headers.TryAddWithoutValidation("Authorization", auth);
    }
}

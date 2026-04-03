using System.Net.Http.Json;
using System.Text.Json;
using Identity.Application.Interfaces.Services;
using Identity.Infrastructure.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Security.Cryptography;

namespace Identity.Infrastructure.Services;

public sealed class EmailService(
    IOptions<EmailOptions> options,
    IOptions<SettingsClientOptions> settingsOptions,
    IHttpClientFactory httpClientFactory,
    ILogger<EmailService> logger) : IEmailService
{
    private readonly EmailOptions _options = options.Value;
    private readonly SettingsClientOptions _settings = settingsOptions.Value;

    public (string Token, DateTime ExpiresAt) GenerateConfirmationToken()
    {
        var tokenBytes = new byte[32];
        RandomNumberGenerator.Fill(tokenBytes);

        var token = Convert.ToBase64String(tokenBytes).Replace("+", "-").Replace("/", "_").TrimEnd('=');

        var expiresAt = DateTime.UtcNow.AddHours(_options.ConfirmationTokenLifeTimeHours);

        return (token, expiresAt);
    }

    public string GetConfirmationLink(string token)
    {
        var baseUrl = _options.BaseBackUrl.TrimEnd('/');
        var path = "api/v1/auth/confirm-email";
        var query = $"token={Uri.EscapeDataString(token)}";
        return $"{baseUrl}/{path}?{query}";
    }

    public string GetPasswordResetLink(string token)
    {
        var baseUrl = _options.BaseFrontUrl.TrimEnd('/');
        var path = "reset-password";
        var query = $"token={Uri.EscapeDataString(token)}";
        return $"{baseUrl}/{path}?{query}";
    }

    public async Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        if (_settings.UseSystemEmailMailbox && !string.IsNullOrWhiteSpace(_settings.SystemEmailSendApiKey))
        {
            try
            {
                await SendViaSettingsSystemMailboxAsync(to, subject, body, cancellationToken);
                return;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex,
                    "System email relay via Settings failed; falling back to Email:Smtp. To: {To}",
                    to);
            }
        }

        await SendDirectSmtpAsync(to, subject, body, cancellationToken);
    }

    private async Task SendViaSettingsSystemMailboxAsync(string to, string subject, string body, CancellationToken cancellationToken)
    {
        var baseUrl = _settings.BaseUrl.TrimEnd('/');
        var client = httpClientFactory.CreateClient("SettingsEmailRelay");
        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{baseUrl}/api/email-settings/system/send");
        request.Headers.TryAddWithoutValidation(SystemEmailRelayConstants.HeaderName, _settings.SystemEmailSendApiKey);
        request.Content = JsonContent.Create(
            new { to, subject, body, isHtml = true },
            options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        var response = await client.SendAsync(request, cancellationToken);
        var responseText = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Settings email relay returned {(int)response.StatusCode}: {responseText}");
    }

    private async Task SendDirectSmtpAsync(string to, string subject, string body, CancellationToken cancellationToken)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(_options.FromDisplayName, _options.From));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html) { Text = body };

        using var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(
            _options.Host,
            _options.Port,
            _options.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None,
            cancellationToken);

        if (!string.IsNullOrWhiteSpace(_options.Username))
            await smtpClient.AuthenticateAsync(_options.Username, _options.Password ?? "", cancellationToken);

        await smtpClient.SendAsync(message, cancellationToken);
        await smtpClient.DisconnectAsync(true, cancellationToken);
    }
}

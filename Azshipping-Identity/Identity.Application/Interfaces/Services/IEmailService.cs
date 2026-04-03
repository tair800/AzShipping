namespace Identity.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
    (string Token, DateTime ExpiresAt) GenerateConfirmationToken();
    string GetConfirmationLink(string token);
    string GetPasswordResetLink(string token);
}
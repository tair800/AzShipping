using MediatR;
using Settings.Application.DTOs.EmailSetting;
using Settings.Application.Features.EmailSettings;
using Settings.Application.Interfaces.Services;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Application.Features.EmailSettings.Commands.Update;

public sealed class UpdateEmailSettingCommandHandler(
    IEmailAccountSettingRepository repository,
    ISmtpMailboxSecretProtector secretProtector)
    : IRequestHandler<UpdateEmailSettingCommand, EmailSettingDetailDto?>
{
    public async Task<EmailSettingDetailDto?> Handle(UpdateEmailSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetForUpdateAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;
        if (string.IsNullOrWhiteSpace(dto.AccountEmail))
            throw new InvalidOperationException("Email is required.");
        if (string.IsNullOrWhiteSpace(dto.SmtpHost))
            throw new InvalidOperationException("Server is required.");
        if (dto.SmtpPort <= 0 || dto.SmtpPort > 65535)
            throw new InvalidOperationException("Port must be between 1 and 65535.");

        var normalized = EmailAccountSetting.NormalizeAccountEmail(dto.AccountEmail);
        var other = await repository.GetByAccountEmailAsync(normalized, cancellationToken);
        if (other != null && other.Id != entity.Id)
            throw new InvalidOperationException("An entry with this email already exists.");

        entity.AccountEmail = normalized;
        entity.UseSeparateAuthLogin = dto.UseSeparateAuthLogin;
        entity.SmtpAuthUsername = string.IsNullOrWhiteSpace(dto.SmtpAuthUsername) ? null : dto.SmtpAuthUsername.Trim();
        entity.WithoutPassword = dto.WithoutPassword;
        entity.ConnectionMode = string.IsNullOrWhiteSpace(dto.ConnectionMode) ? "Manual" : dto.ConnectionMode.Trim();
        entity.SmtpHost = dto.SmtpHost.Trim();
        entity.SmtpPort = dto.SmtpPort;
        entity.SmtpSecurity = string.IsNullOrWhiteSpace(dto.SmtpSecurity) ? "StartTls" : dto.SmtpSecurity.Trim();
        entity.IsSystemEmail = dto.IsSystemEmail;
        entity.IdentityUserId = dto.IdentityUserId;
        entity.LinkedUserDisplayName = string.IsNullOrWhiteSpace(dto.LinkedUserDisplayName) ? null : dto.LinkedUserDisplayName.Trim();
        entity.UpdatedAtUtc = DateTime.UtcNow;

        if (dto.WithoutPassword)
            entity.ProtectedPassword = null;
        else if (dto.ChangePassword)
        {
            if (string.IsNullOrEmpty(dto.Password))
                throw new InvalidOperationException("Enter a new password, or clear \"Without password\" only if you intend to remove SMTP auth.");
            entity.ProtectedPassword = secretProtector.Protect(dto.Password);
        }

        await repository.UpdateAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return EmailSettingMapper.ToDetail(loaded ?? entity);
    }
}

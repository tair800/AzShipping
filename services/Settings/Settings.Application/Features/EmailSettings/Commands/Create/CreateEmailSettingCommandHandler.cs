using MediatR;
using Settings.Application.DTOs.EmailSetting;
using Settings.Application.Features.EmailSettings;
using Settings.Application.Interfaces.Services;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Application.Features.EmailSettings.Commands.Create;

public sealed class CreateEmailSettingCommandHandler(
    IEmailAccountSettingRepository repository,
    ISmtpMailboxSecretProtector secretProtector)
    : IRequestHandler<CreateEmailSettingCommand, EmailSettingDetailDto>
{
    public async Task<EmailSettingDetailDto> Handle(CreateEmailSettingCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        if (string.IsNullOrWhiteSpace(dto.AccountEmail))
            throw new InvalidOperationException("Email is required.");
        if (string.IsNullOrWhiteSpace(dto.SmtpHost))
            throw new InvalidOperationException("Server is required.");
        if (dto.SmtpPort <= 0 || dto.SmtpPort > 65535)
            throw new InvalidOperationException("Port must be between 1 and 65535.");
        if (!dto.WithoutPassword && string.IsNullOrEmpty(dto.Password))
            throw new InvalidOperationException("Password is required unless \"Without password\" is enabled.");

        var normalized = EmailAccountSetting.NormalizeAccountEmail(dto.AccountEmail);
        var dup = await repository.GetByAccountEmailAsync(normalized, cancellationToken);
        if (dup != null)
            throw new InvalidOperationException("An entry with this email already exists.");

        var now = DateTime.UtcNow;
        var entity = new EmailAccountSetting
        {
            Id = Guid.NewGuid(),
            AccountEmail = normalized,
            UseSeparateAuthLogin = dto.UseSeparateAuthLogin,
            SmtpAuthUsername = string.IsNullOrWhiteSpace(dto.SmtpAuthUsername) ? null : dto.SmtpAuthUsername.Trim(),
            ProtectedPassword = dto.WithoutPassword ? null : secretProtector.Protect(dto.Password),
            WithoutPassword = dto.WithoutPassword,
            ConnectionMode = string.IsNullOrWhiteSpace(dto.ConnectionMode) ? "Manual" : dto.ConnectionMode.Trim(),
            SmtpHost = dto.SmtpHost.Trim(),
            SmtpPort = dto.SmtpPort,
            SmtpSecurity = string.IsNullOrWhiteSpace(dto.SmtpSecurity) ? "StartTls" : dto.SmtpSecurity.Trim(),
            IsSystemEmail = dto.IsSystemEmail,
            IdentityUserId = dto.IdentityUserId,
            LinkedUserDisplayName = string.IsNullOrWhiteSpace(dto.LinkedUserDisplayName) ? null : dto.LinkedUserDisplayName.Trim(),
            CreatedAtUtc = now
        };

        await repository.AddAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return EmailSettingMapper.ToDetail(loaded ?? entity);
    }
}

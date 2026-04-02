namespace Settings.Domain.AggregatesModel.EmailAccountAggregate;

/// <summary>
/// SMTP mailbox configuration (Figma: Email settings). <see cref="IdentityUserId"/> references Azshipping-Identity <c>User.Id</c> (no FK).
/// </summary>
public class EmailAccountSetting
{
    public static string NormalizeAccountEmail(string email) => email.Trim().ToLowerInvariant();

    public Guid Id { get; set; }

    /// <summary>Mailbox / From address shown in the grid.</summary>
    public string AccountEmail { get; set; } = string.Empty;

    /// <summary>Use a different login than <see cref="AccountEmail"/> for SMTP auth.</summary>
    public bool UseSeparateAuthLogin { get; set; }

    /// <summary>SMTP auth user when <see cref="UseSeparateAuthLogin"/>; otherwise typically same as account.</summary>
    public string? SmtpAuthUsername { get; set; }

    /// <summary>DP-protected SMTP password; null if <see cref="WithoutPassword"/> or not set.</summary>
    public byte[]? ProtectedPassword { get; set; }

    public bool WithoutPassword { get; set; }

    /// <summary>e.g. Manual, PresetGmail — for UI presets.</summary>
    public string ConnectionMode { get; set; } = "Manual";

    public string SmtpHost { get; set; } = string.Empty;
    public int SmtpPort { get; set; } = 587;

    /// <summary>None, StartTls, Ssl (implicit SSL / port 465 style).</summary>
    public string SmtpSecurity { get; set; } = "StartTls";

    public bool IsSystemEmail { get; set; }

    /// <summary>Identity user id when this row is a person's mailbox.</summary>
    public long? IdentityUserId { get; set; }

    /// <summary>Denormalized display name for list (set by client from Identity picker).</summary>
    public string? LinkedUserDisplayName { get; set; }

    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
}

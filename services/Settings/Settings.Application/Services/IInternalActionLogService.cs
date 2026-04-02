namespace Settings.Application.Services;

/// <summary>Internal service to log user actions to the local Action Log (Settings service owns the log).</summary>
public interface IInternalActionLogService
{
    /// <summary>Logs an action. Does not throw; failures are logged but do not affect the caller.</summary>
    Task LogAsync(string action, string data, Guid? employeeId = null, string? employeeName = null, CancellationToken cancellationToken = default);
}

namespace General.Application.Services;

/// <summary>Client to log user actions to the Settings service Action Log.</summary>
public interface IActionLogClient
{
    /// <summary>Logs an action. Does not throw; failures are logged but do not affect the caller.</summary>
    Task LogAsync(string action, string data, Guid? employeeId = null, string? employeeName = null, CancellationToken cancellationToken = default);
}

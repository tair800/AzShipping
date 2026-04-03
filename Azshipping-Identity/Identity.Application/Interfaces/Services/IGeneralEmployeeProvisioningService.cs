namespace Identity.Application.Interfaces.Services;

/// <summary>
/// Creates a matching <c>Employee</c> row in General.API when an Identity user is marked as an employee.
/// </summary>
public interface IGeneralEmployeeProvisioningService
{
    /// <summary>
    /// POSTs to General <c>/api/employees</c>. Errors are logged; the Identity user is already persisted.
    /// </summary>
    Task TryProvisionEmployeeAsync(
        long identityUserId,
        string username,
        string? fullName,
        string email,
        string? phone,
        Guid? departmentId,
        Guid? workerPostId,
        CancellationToken cancellationToken);
}

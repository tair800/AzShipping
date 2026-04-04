namespace Identity.Domain.SeedData;

/// <summary>Local dev user for ERP Request permission tests. Employee group must exist in Settings with the same <see cref="EmployeeGroupId"/>.</summary>
public static class ErpTestRequestViewer
{
    /// <summary>Matches Settings seed <c>EnsureErpTestEmployeeGroupAsync</c>.</summary>
    public static readonly Guid EmployeeGroupId = Guid.Parse("a0000001-0001-4001-8001-000000000001");

    public const string Username = "requestviewer";
    public const string Password = "RequestViewer1!";
    public const string Email = "requestviewer@local.test";
}

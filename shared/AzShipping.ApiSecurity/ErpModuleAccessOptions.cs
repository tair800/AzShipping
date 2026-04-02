namespace AzShipping.ApiSecurity;

public sealed class ErpModuleAccessOptions
{
    public const string SectionName = "ErpModuleAccess";

    /// <summary>When false, middleware is a no-op.</summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// JSON module keys from employee-group permissions (e.g. <c>Clients</c>, <c>Request</c>).
    /// User must have at least one <c>erp_permission</c> claim matching any prefix (exact, <c>Prefix.</c>, or <c>Prefix=</c>).
    /// </summary>
    public string[] ModulePrefixes { get; set; } = [];
}

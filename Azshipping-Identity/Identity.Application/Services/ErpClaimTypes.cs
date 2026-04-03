namespace Identity.Application.Services;

/// <summary>JWT claim types for Settings employee-group permissions (flattened paths).</summary>
public static class ErpClaimTypes
{
    public const string Permission = "erp_permission";

    /// <summary>When present (value <c>1</c>), user bypasses ERP matrix checks.</summary>
    public const string Unlimited = "erp_unlimited";

    public const string ResolvePermissionsHeaderName = "X-AzShipping-Employee-Groups-Resolve-Key";
}

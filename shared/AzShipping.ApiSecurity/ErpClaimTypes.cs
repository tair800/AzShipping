namespace AzShipping.ApiSecurity;

/// <summary>Must match Identity token claims: <c>erp_permission</c>, <c>erp_unlimited</c>.</summary>
public static class ErpClaimTypes
{
    public const string Permission = "erp_permission";
    public const string Unlimited = "erp_unlimited";
}

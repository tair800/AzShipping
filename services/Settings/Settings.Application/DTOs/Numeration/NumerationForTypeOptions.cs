namespace Settings.Application.DTOs.Numeration;

/// <summary>Predefined numeration-for options for dropdowns.</summary>
public static class NumerationForTypeOptions
{
    public static readonly (string Code, string Name)[] All =
    [
        ("ForRequest", "For request"),
        ("ForOrder", "For order"),
        ("IssuedInvoices", "Issued invoices"),
        ("ContractWithCarrier", "Contract with carrier"),
        ("ContractWithClient", "Contract with client"),
        ("ConsolidationAutoTrip", "Consolidation: Auto trip"),
        ("ConsolidationRailwayTrip", "Consolidation: Railway trip"),
        ("ConsolidationSeaTrips", "Consolidation: Sea trips"),
    ];
}

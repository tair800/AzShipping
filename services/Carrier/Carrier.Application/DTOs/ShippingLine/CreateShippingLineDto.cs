namespace Carrier.Application.DTOs.ShippingLine;

public class CreateShippingLineDto
{
    public string? Code { get; set; }
    public string? ScacCode { get; set; }
    public string? Cbsa { get; set; }
    public string? Caat { get; set; }
    public string? Name { get; set; }
    public string? LocalName { get; set; }
    public string? ShippingAgent { get; set; }
    public Guid? ShippingAgentCompanyId { get; set; }
    public string? Website { get; set; }
    public string? VatNo { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
}

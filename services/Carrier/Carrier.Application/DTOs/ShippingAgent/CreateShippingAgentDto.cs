namespace Carrier.Application.DTOs.ShippingAgent;

public class CreateShippingAgentDto
{
    public string? CompanyName { get; set; }
    public string? LocalName { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public Guid? CountryId { get; set; }
    public Guid? StateId { get; set; }
    public Guid? CityId { get; set; }
    public string? ZipCode { get; set; }
    public string? VatNo { get; set; }
    public string? Email { get; set; }
    public string? EnglishName { get; set; }
    public string? Position { get; set; }
    public string? BusinessPhone { get; set; }
    public string? Mobile { get; set; }
    public string? Fax { get; set; }
    public string? Phone { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
}

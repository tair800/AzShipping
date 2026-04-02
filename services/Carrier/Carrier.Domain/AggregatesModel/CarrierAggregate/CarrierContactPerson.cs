namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

public class CarrierContactPerson
{
    public Guid Id { get; set; }
    public Guid CarrierId { get; set; }
    public Carrier Carrier { get; set; } = null!;
    public string? EnglishName { get; set; }
    public string? Position { get; set; }
    public string? Emails { get; set; }    // Semicolon-separated for multiple
    public string? Phones { get; set; }
    public string? Fax { get; set; }
}

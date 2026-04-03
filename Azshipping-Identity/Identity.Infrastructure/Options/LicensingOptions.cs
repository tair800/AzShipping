namespace Identity.Infrastructure.Options;

public sealed class LicensingOptions
{
    public const string SectionName = "Licensing";

    /// <summary>When set, activation (email confirmation or admin immediate activate) cannot exceed this many <see cref="Identity.Domain.AggregatesModel.UserAggregate.Enumerations.UserStatus.Active"/> users.</summary>
    public int? MaxActivatedUsers { get; set; }
}

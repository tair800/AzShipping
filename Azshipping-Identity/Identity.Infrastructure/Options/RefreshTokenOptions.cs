namespace Identity.Infrastructure.Options;

public class RefreshTokenOptions
{
    public int LifeTimeDays { get; set; } = 30;
    public bool RotateOnUse { get; set; } = true;
    public bool RevokeDescendantsOnReuse { get; set; } = true;
}
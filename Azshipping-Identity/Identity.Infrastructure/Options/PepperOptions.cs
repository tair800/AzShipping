namespace Identity.Infrastructure.Options;

public sealed class PepperOptions
{
    public byte[] Pepper { get; init; } = [];
    public int Version { get; init; } = 1;
}
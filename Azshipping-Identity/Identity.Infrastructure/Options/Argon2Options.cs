namespace Identity.Infrastructure.Options;

public class Argon2Options
{
    public int Iterations { get; init; } = 3;
    public int MemorySize { get; init; } = 65536;
    public int DegreeOfParallelism { get; init; } = 1;
    public int SaltLength { get; init; } = 16;
    public int HashLength { get; init; } = 32;
}
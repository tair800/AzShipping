using Identity.Application.Interfaces.Services;
using Identity.Infrastructure.Options;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Infrastructure.Services;

public class PasswordService(IOptions<Argon2Options> options, PepperOptions pepperOptions) : IPasswordService
{
    private const string Algorithm = "argon2id";
    private const string Version = "v=19";

    private readonly Argon2Options _argon2Settings = options.Value;
    private readonly PepperOptions _pepperOptions = pepperOptions;

    public string HashPassword(string password)
    {
        var salt = GenerateSalt();

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        try
        {
            var hash = ComputeHash(
            passwordBytes,
            salt,
            _argon2Settings.Iterations,
            _argon2Settings.DegreeOfParallelism,
            _argon2Settings.MemorySize,
            _argon2Settings.HashLength
            );

            var passwordHash = $"${Algorithm}${Version}$m={_argon2Settings.MemorySize},t={_argon2Settings.Iterations},p={_argon2Settings.DegreeOfParallelism},pv={_pepperOptions.Version}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";

            return passwordHash;
        }
        finally
        {
            CryptographicOperations.ZeroMemory(passwordBytes.AsSpan());
        }
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        try
        {
            var parts = hashedPassword.Split('$');

            if (parts.Length != 6) return false;
            if (!string.Equals(parts[1], Algorithm, StringComparison.Ordinal)) return false;
            if (!string.Equals(parts[2], "v=19", StringComparison.Ordinal)) return false;

            var paramMap = parts[3].Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split('=', 2))
                .ToDictionary(a => a[0], a => a[1], StringComparer.Ordinal);

            if (!int.TryParse(paramMap["m"], out var memoryKb)) return false;
            if (!int.TryParse(paramMap["t"], out var iterations)) return false;
            if (!int.TryParse(paramMap["p"], out var parallelism)) return false;

            parallelism = Math.Clamp(parallelism, 1, Environment.ProcessorCount);

            var salt = Convert.FromBase64String(parts[4]);
            var expectedHash = Convert.FromBase64String(parts[5]);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            try
            {
                var actualHash = ComputeHash(
                passwordBytes,
                salt,
                iterations,
                parallelism,
                memoryKb,
                expectedHash.Length
                );

                try
                {
                    var isEqual = CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
                    return isEqual;
                }
                finally
                {
                    CryptographicOperations.ZeroMemory(actualHash.AsSpan());
                }
            }
            finally
            {
                CryptographicOperations.ZeroMemory(passwordBytes.AsSpan());
            }
        }
        catch
        {
            return false;
        }
    }

    public bool NeedsRehash(string hashedPassword)
    {
        try
        {
            var parts = hashedPassword.Split('$');
            if (parts.Length != 6) return true;

            if (!string.Equals(parts[1], "argon2id", StringComparison.Ordinal)) return true;
            if (!string.Equals(parts[2], "v=19", StringComparison.Ordinal)) return true;

            var map = parts[3].Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split('=', 2))
                .ToDictionary(a => a[0], a => a[1], StringComparer.Ordinal);

            if (!map.TryGetValue("m", out var mStr) || !int.TryParse(mStr, out var m)) return true;
            if (!map.TryGetValue("t", out var tStr) || !int.TryParse(tStr, out var t)) return true;
            if (!map.TryGetValue("p", out var pStr) || !int.TryParse(pStr, out var p)) return true;

            var pv = map.TryGetValue("pv", out var pvStr) && int.TryParse(pvStr, out var pvVal) ? pvVal : 1;

            var actualHashLen = Convert.FromBase64String(parts[5]).Length;

            if (m < _argon2Settings.MemorySize) return true;
            if (t < _argon2Settings.Iterations) return true;
            if (p < _argon2Settings.DegreeOfParallelism) return true;
            if (actualHashLen != _argon2Settings.HashLength) return true;
            if (pv != _pepperOptions.Version) return true;

            return false;
        }
        catch
        {
            return true;
        }
    }

    private byte[] GenerateSalt()
    {
        var salt = RandomNumberGenerator.GetBytes(_argon2Settings.SaltLength);
        return salt;
    }

    private byte[] ComputeHash(byte[] passwordBytes, byte[] salt, int iterations, int parallelism, int memoryKb, int hashSize)
    {
        parallelism = Math.Clamp(parallelism, 1, Environment.ProcessorCount);

        using var argon2 = new Argon2id(passwordBytes)
        {
            Salt = salt,
            Iterations = iterations,
            DegreeOfParallelism = parallelism,
            MemorySize = memoryKb,
            KnownSecret = _pepperOptions.Pepper
        };
        return argon2.GetBytes(hashSize);
    }
}
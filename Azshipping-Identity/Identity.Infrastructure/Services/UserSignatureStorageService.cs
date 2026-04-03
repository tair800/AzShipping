using Identity.Application.Interfaces.Services;
using Microsoft.Extensions.Hosting;
using MrStyx.Application.Exceptions;

namespace Identity.Infrastructure.Services;

public sealed class UserSignatureStorageService(IHostEnvironment env) : IUserSignatureStorageService
{
    private static readonly string[] AllowedExtensions = [".png", ".jpg", ".jpeg", ".gif", ".webp", ".svg"];

    public async Task<string> SaveAsync(long userId, Stream content, string originalFileName, CancellationToken cancellationToken)
    {
        var ext = Path.GetExtension(originalFileName)?.ToLowerInvariant() ?? "";
        if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
            throw new BadRequestException(
                $"Signature file type not allowed. Use: {string.Join(", ", AllowedExtensions)}");

        var safeName = $"signature_{DateTime.UtcNow:yyyyMMddHHmmss}{ext}";
        var relativeDir = Path.Combine("wwwroot", "uploads", "signatures", userId.ToString());
        var physicalDir = Path.Combine(env.ContentRootPath, relativeDir);
        Directory.CreateDirectory(physicalDir);
        var physicalPath = Path.Combine(physicalDir, safeName);
        await using (var fs = File.Create(physicalPath))
        {
            await content.CopyToAsync(fs, cancellationToken);
        }

        var webPath = "/uploads/signatures/" + userId + "/" + safeName;
        return webPath.Replace('\\', '/');
    }
}

namespace Identity.Application.Interfaces.Services;

public interface IUserSignatureStorageService
{
    /// <summary>Saves the file and returns the public URL path (e.g. /uploads/signatures/1/file.png).</summary>
    Task<string> SaveAsync(long userId, Stream content, string originalFileName, CancellationToken cancellationToken);
}

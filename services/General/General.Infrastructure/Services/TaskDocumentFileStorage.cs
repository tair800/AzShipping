using General.Application.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace General.Infrastructure.Services;

public sealed class TaskDocumentFileStorage(IHostEnvironment env, IOptions<TaskDocumentStorageOptions> options)
    : ITaskDocumentStorage
{
    private readonly string _root = Path.GetFullPath(Path.Combine(env.ContentRootPath,
        options.Value.RootRelativePath ?? "App_Data/task-documents"));

    public string GetFullPath(string relativeStoredPath)
    {
        if (string.IsNullOrWhiteSpace(relativeStoredPath))
            throw new ArgumentException("Path required.", nameof(relativeStoredPath));
        var combined = Path.GetFullPath(Path.Combine(_root, relativeStoredPath));
        if (!combined.StartsWith(_root, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Invalid stored path.");
        return combined;
    }

    public async Task<string> SaveAsync(Guid taskId, string originalFileName, Stream content,
        CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_root);
        var safe = SanitizeFileName(originalFileName);
        var folder = Path.Combine(_root, taskId.ToString("D"));
        Directory.CreateDirectory(folder);
        var storedFile = $"{Guid.NewGuid():N}_{safe}";
        var full = Path.Combine(folder, storedFile);
        await using (var fs = new FileStream(full, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            await content.CopyToAsync(fs, cancellationToken);
        return Path.Combine(taskId.ToString("D"), storedFile);
    }

    private static string SanitizeFileName(string originalFileName)
    {
        var name = Path.GetFileName(originalFileName);
        if (string.IsNullOrWhiteSpace(name)) name = "file";
        foreach (var c in Path.GetInvalidFileNameChars())
            name = name.Replace(c, '_');
        if (name.Length > 120) name = name[..120];
        return name;
    }
}

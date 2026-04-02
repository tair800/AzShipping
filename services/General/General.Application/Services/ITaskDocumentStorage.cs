namespace General.Application.Services;

/// <summary>Stores binary files for <see cref="General.Domain.AggregatesModel.TaskAggregate.TaskDocument"/>.</summary>
public interface ITaskDocumentStorage
{
    /// <summary>Writes the file under the task folder; returns a path fragment persisted in <c>TaskDocument.FilePath</c> (relative to storage root).</summary>
    Task<string> SaveAsync(Guid taskId, string originalFileName, Stream content, CancellationToken cancellationToken = default);

    string GetFullPath(string relativeStoredPath);
}

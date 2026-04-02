using General.Application.DTOs.Task;
using General.Application.Services;
using General.Domain.AggregatesModel.TaskAggregate;
using MediatR;

namespace General.Application.Features.Tasks.Commands.UploadDocument;

public sealed class UploadTaskDocumentCommandHandler(
    ITaskRepository taskRepository,
    ITaskDocumentRepository documentRepository,
    ITaskDocumentStorage storage)
    : IRequestHandler<UploadTaskDocumentCommand, TaskDocumentDto?>
{
    public async Task<TaskDocumentDto?> Handle(UploadTaskDocumentCommand request, CancellationToken cancellationToken)
    {
        if (request.Content is not { Length: > 0 })
            return null;

        var task = await taskRepository.GetByIdAsync(request.TaskId, cancellationToken);
        if (task == null)
            return null;

        var displayName = request.OriginalFileName?.Trim();
        if (string.IsNullOrEmpty(displayName))
            displayName = "attachment";
        if (displayName.Length > 200)
            displayName = displayName[..200];

        await using var ms = new MemoryStream(request.Content, writable: false);
        var relative = await storage.SaveAsync(request.TaskId, request.OriginalFileName ?? "file", ms, cancellationToken);

        var doc = new TaskDocument
        {
            Id = Guid.NewGuid(),
            TaskId = request.TaskId,
            FilePath = relative.Replace('\\', '/'),
            DocumentName = displayName,
            CreatedAt = DateTime.UtcNow
        };

        await documentRepository.AddAsync(doc, cancellationToken);

        return new TaskDocumentDto
        {
            Id = doc.Id,
            FilePath = doc.FilePath,
            DocumentName = doc.DocumentName
        };
    }
}

using General.Application.DTOs.Task;
using MediatR;

namespace General.Application.Features.Tasks.Commands.UploadDocument;

public sealed record UploadTaskDocumentCommand(Guid TaskId, string OriginalFileName, byte[] Content)
    : IRequest<TaskDocumentDto?>;

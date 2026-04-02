using Clients.Application.DTOs.Document;
using Clients.Application.Services;
using Clients.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace Clients.Application.Features.Documents.Commands.Update;

public sealed class UpdateDocumentCommandHandler(IDocumentRepository repository, IActionLogClient actionLogClient) : IRequestHandler<UpdateDocumentCommand, DocumentDto?>
{
    public async Task<DocumentDto?> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;
        entity.CompanyId = dto.CompanyId;
        entity.DocumentType = dto.DocumentType ?? entity.DocumentType;
        entity.TemplateId = dto.TemplateId;
        entity.DocumentNumber = dto.DocumentNumber ?? entity.DocumentNumber;
        if (!string.IsNullOrWhiteSpace(dto.DocumentDate) && DateTime.TryParse(dto.DocumentDate, null, System.Globalization.DateTimeStyles.RoundtripKind, out var parsed))
            entity.DocumentDate = parsed.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(parsed, DateTimeKind.Utc) : parsed.ToUniversalTime();
        entity.DocumentName = dto.DocumentName ?? entity.DocumentName;
        entity.ValidFrom = dto.ValidFrom;
        entity.ValidUntil = dto.ValidUntil;
        entity.ExpirationDate = dto.ExpirationDate ?? dto.ValidUntil;
        entity.NotifyUserId = dto.NotifyUserId;
        entity.ProhibitOnExpiry = dto.ProhibitOnExpiry;
        entity.IsDefault = dto.IsDefault;
        entity.Comments = dto.Comments;
        entity.AvailableForClient = dto.AvailableForClient;
        entity.IsSent = dto.IsSent;
        if (dto.FilePath != null) entity.FilePath = dto.FilePath;

        await repository.UpdateAsync(entity, cancellationToken);
        await actionLogClient.LogAsync("Client document updated", $"document: {entity.DocumentName} • type: {entity.DocumentType} • id: {entity.Id}", null, null, cancellationToken);
        return new DocumentDto
        {
            Id = entity.Id,
            ClientId = entity.ClientId,
            CompanyId = entity.CompanyId,
            DocumentType = entity.DocumentType,
            TemplateId = entity.TemplateId,
            DocumentNumber = entity.DocumentNumber,
            DocumentDate = entity.DocumentDate,
            DocumentName = entity.DocumentName,
            CreatedAt = entity.CreatedAt,
            ValidFrom = entity.ValidFrom,
            ValidUntil = entity.ValidUntil,
            ExpirationDate = entity.ExpirationDate,
            NotifyUserId = entity.NotifyUserId,
            ProhibitOnExpiry = entity.ProhibitOnExpiry,
            IsDefault = entity.IsDefault,
            Comments = entity.Comments,
            AvailableForClient = entity.AvailableForClient,
            IsSent = entity.IsSent,
            FilePath = entity.FilePath
        };
    }
}

using Clients.Application.DTOs.Document;
using Clients.Application.Services;
using Clients.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace Clients.Application.Features.Documents.Commands.Create;

public sealed class CreateDocumentCommandHandler(IDocumentRepository repository, IActionLogClient actionLogClient) : IRequestHandler<CreateDocumentCommand, DocumentDto>
{
    public async Task<DocumentDto> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var docDate = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(dto.DocumentDate) && DateTime.TryParse(dto.DocumentDate, null, System.Globalization.DateTimeStyles.RoundtripKind, out var parsed))
            docDate = parsed.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(parsed, DateTimeKind.Utc) : parsed.ToUniversalTime();

        var entity = new Document
        {
            Id = Guid.NewGuid(),
            ClientId = dto.ClientId,
            CompanyId = dto.CompanyId,
            DocumentType = dto.DocumentType ?? "upload",
            TemplateId = dto.TemplateId,
            DocumentNumber = dto.DocumentNumber ?? "",
            DocumentDate = docDate,
            DocumentName = dto.DocumentName ?? "",
            CreatedAt = DateTime.UtcNow,
            ValidFrom = dto.ValidFrom,
            ValidUntil = dto.ValidUntil,
            ExpirationDate = dto.ExpirationDate ?? dto.ValidUntil,
            NotifyUserId = dto.NotifyUserId,
            ProhibitOnExpiry = dto.ProhibitOnExpiry,
            IsDefault = dto.IsDefault,
            Comments = dto.Comments,
            AvailableForClient = dto.AvailableForClient,
            IsSent = dto.IsSent
        };
        await repository.AddAsync(entity, cancellationToken);
        var result = MapToDto(entity);
        await actionLogClient.LogAsync("Client document created", $"document: {entity.DocumentName} • type: {entity.DocumentType} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }

    private static DocumentDto MapToDto(Document e) => new()
    {
        Id = e.Id,
        ClientId = e.ClientId,
        CompanyId = e.CompanyId,
        DocumentType = e.DocumentType,
        TemplateId = e.TemplateId,
        DocumentNumber = e.DocumentNumber,
        DocumentDate = e.DocumentDate,
        DocumentName = e.DocumentName,
        CreatedAt = e.CreatedAt,
        ValidFrom = e.ValidFrom,
        ValidUntil = e.ValidUntil,
        ExpirationDate = e.ExpirationDate,
        NotifyUserId = e.NotifyUserId,
        ProhibitOnExpiry = e.ProhibitOnExpiry,
        IsDefault = e.IsDefault,
        Comments = e.Comments,
        AvailableForClient = e.AvailableForClient,
        IsSent = e.IsSent,
        FilePath = e.FilePath
    };
}

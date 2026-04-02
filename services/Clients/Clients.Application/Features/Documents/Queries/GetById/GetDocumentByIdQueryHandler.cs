using Clients.Application.DTOs.Document;
using Clients.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace Clients.Application.Features.Documents.Queries.GetById;

public sealed class GetDocumentByIdQueryHandler(IDocumentRepository repository) : IRequestHandler<GetDocumentByIdQuery, DocumentDto?>
{
    public async Task<DocumentDto?> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
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

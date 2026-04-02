using Clients.Application.DTOs.Document;
using Clients.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace Clients.Application.Features.Documents.Queries.GetByClientId;

public sealed class GetDocumentsByClientIdQueryHandler(IDocumentRepository repository) : IRequestHandler<GetDocumentsByClientIdQuery, IReadOnlyList<DocumentDto>>
{
    public async Task<IReadOnlyList<DocumentDto>> Handle(GetDocumentsByClientIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetByClientIdAsync(request.ClientId, cancellationToken);
        return entities.Select(e => new DocumentDto
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
        }).ToList();
    }
}

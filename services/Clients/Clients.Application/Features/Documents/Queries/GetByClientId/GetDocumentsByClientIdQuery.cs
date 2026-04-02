using Clients.Application.DTOs.Document;
using MediatR;

namespace Clients.Application.Features.Documents.Queries.GetByClientId;

public sealed record GetDocumentsByClientIdQuery(Guid ClientId) : IRequest<IReadOnlyList<DocumentDto>>;

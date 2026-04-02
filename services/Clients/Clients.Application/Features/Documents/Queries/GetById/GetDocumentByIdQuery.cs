using Clients.Application.DTOs.Document;
using MediatR;

namespace Clients.Application.Features.Documents.Queries.GetById;

public sealed record GetDocumentByIdQuery(Guid Id) : IRequest<DocumentDto?>;

using Clients.Application.DTOs.Document;
using MediatR;

namespace Clients.Application.Features.Documents.Commands.Update;

public sealed record UpdateDocumentCommand(Guid Id, UpdateDocumentDto Dto) : IRequest<DocumentDto?>;

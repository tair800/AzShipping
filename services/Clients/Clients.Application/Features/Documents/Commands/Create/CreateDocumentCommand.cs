using Clients.Application.DTOs.Document;
using MediatR;

namespace Clients.Application.Features.Documents.Commands.Create;

public sealed record CreateDocumentCommand(CreateDocumentDto Dto) : IRequest<DocumentDto>;
